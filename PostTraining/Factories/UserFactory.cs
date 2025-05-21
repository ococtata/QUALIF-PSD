using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Factories
{
    public class UserFactory
    {
        public User CreateUser(String name, String email, String password, String role)
        {
            Guid id = Guid.NewGuid();
            User user = new User()
            {
                Id = id,
                Name = name,   
                Email = email,
                Password = password,
                Role = role
            };

            return user;
        }
    }
}