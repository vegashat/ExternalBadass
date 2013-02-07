using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExternalBadass.ViewModels
{
    public class UserActivityViewModel
    {
        public UserActivityViewModel(IEnumerable<User> users, IEnumerable<Activity> activities, User currentUser)
        {
            Users = users;
            Activities = activities;
            CurrentUser = currentUser;
        }

        public IEnumerable<User> Users { get; set; }


        public IEnumerable<Activity> Activities { get; set; }

        public User CurrentUser { get; set; }

        public SelectList UserSelectList {
            get
            {
                return new SelectList(Users, "UserId", "Username", CurrentUser.UserId);
            }
        }
       
        public SelectList ActivitySelectList
        {
            get
            {
                return new SelectList(Activities, "ActvityId", "Name");
            }
        }

    }
}