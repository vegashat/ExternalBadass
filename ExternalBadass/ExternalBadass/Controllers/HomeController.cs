using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using ExternalBadass.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ExternalBadass.Controllers
{
    public class HomeController : Controller
    {

        UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        //
        // GET: /Home/
        private static OpenIdRelyingParty _openId = new OpenIdRelyingParty();
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return View("Login");
            }

            return View();
        }


        public ActionResult Login()
        {

            return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            string googleOpenId = "https://www.google.com/accounts/o8/id";
            var response = _openId.GetResponse();

            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(googleOpenId, out id))
                {
                    try
                    {
                        var request = _openId.CreateRequest(googleOpenId);

                        request.AddExtension(new ClaimsRequest()
                        {
                            Email = DemandLevel.Require
                        });

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewData["Message"] = ex.Message;
                        return View("Login");
                    }
                }
                else
                {
                    ViewData["Message"] = "Invalid identifier";
                    return View("Login");
                }
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;

                        var sreg = response.GetExtension<ClaimsResponse>();

                        if (sreg != null)
                        {
                            // Do something with the values here, like store them in your database for this user.
                            var userObj = _userService.GetUser(sreg.Email);

                            if (userObj == null)
                            {
                                userObj = new Models.User();

                                userObj.FullName = sreg.FullName;
                                userObj.Email = sreg.Email;
                                userObj.Username = sreg.Nickname;
                                userObj.Birthday = sreg.BirthDate ?? DateTime.Now;
                                userObj.Gender = sreg.Gender  ?? Gender.Male;

                                _userService.SaveUser(userObj);

                                
                            }

                            if (userObj.FullName == null)
                            {
                                return RedirectToAction("EditById", "User", new { userId = userObj.UserId });
                            }

                            var userData = string.Format("{0};{1};{2}", userObj.UserId, userObj.FullName, userObj.Email);
                            
                            
                            var ticket = new FormsAuthenticationTicket(
                                           2, // magic number used by FormsAuth
                                           userObj.Username, // username
                                           DateTime.Now,
                                           DateTime.Now.AddDays(30),
                                           true, // "remember me"
                                           userData);

                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                            if (ticket.IsPersistent)
                            {
                                cookie.Expires = ticket.Expiration;
                            }
                            Response.SetCookie(cookie);
                        }


                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("Login");
                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("Login");
                }
            }

            return new EmptyResult();
        }

    }
}
