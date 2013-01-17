using ExternalBadass.Models;
using ExternalBadass.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExternalBadass.Controllers
{
    public class UserController : Controller
    {
        UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        public ActionResult Edit(int userId)
        {
            var user = _userService.GetUser(userId);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            _userService.SaveUser(user);

            return View(user);
        }
    }
}
