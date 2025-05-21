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
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
            {
                return null;
            }

            User user = db.Users.Find(guidId);
            return user;
        }
    }
}