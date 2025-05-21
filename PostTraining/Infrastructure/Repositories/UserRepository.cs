using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Infrastructure.Repositories
{
    public class UserRepository
    {
        private LocalDatabaseEntities1 db = Database.GetInstance();
        public User GetUserByEmail(String email)
        {
            User user  = db.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User GetUserById(String id)
        {
            User user = db.Users.Find(id);
            return user;
        }
    }
}