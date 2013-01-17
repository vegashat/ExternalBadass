using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ExternalBadass
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        public override void Init()
        {
            base.Init();
            this.AuthenticateRequest += new EventHandler(MvcApplication_AuthenticateRequest);
            this.PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
        }

        void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
                    BadassIdentity id = new BadassIdentity(ticket);
                    GenericPrincipal prin = new GenericPrincipal(id, null);
                    HttpContext.Current.User = prin;
                }
            }
        }


    }
}