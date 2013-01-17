using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Services
{
    public class UserService
    {
        public BadassContext _context;

        public UserService()
        {
            _context = new BadassContext();
        }

        public User GetUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }

        public void SaveUser(User user)
        {
            if (user.UserId == 0)
            {

                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Attach(user);
                _context.Entry<User>(user).State = System.Data.EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
    }
}