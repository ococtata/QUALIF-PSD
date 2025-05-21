using PostTraining.Application.Common;
using PostTraining.Domain.Models;
using PostTraining.Factories;
using PostTraining.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PostTraining.Application.Handlers
{
    public class UserHandler
    {
        
        private UserRepository userRepo = new UserRepository();
        private UserFactory userFactory = new UserFactory();
        public Response<User> Login(String email, String password)
        {
            User user = userRepo.GetUserByEmail(email);

            if (user == null)
            {
                return new Response<User>()
                {
                    Success = false,
                    Message = "User with email " + email + " not found",
                    Payload = null,
                };
            }

            return new Response<User>()
            {
                Success = true,
                Message = "Login success",
                Payload = user,
            };
        }

        public Response<User> Register(String name, String email, String password)
        {
            String role = "customer";
            User user = userRepo.GetUserByEmail(email);

            if (user != null)
            {
                return new Response<User>()
                {
                    Success = false,
                    Message = "User with email " + email + " already registered",
                    Payload = null,
                };
            }

            user = userFactory.CreateUser(name, email, password, role);
            userRepo.CreateUser(user);

            return new Response<User>()
            {
                Success = true,
                Message = "Register success",
                Payload = user,
            };
        }

        public Response<User> LoginUserByCookie(String cookie)
        {
            String userId = CookieHelper.Decrypt(cookie);
            System.Diagnostics.Debug.WriteLine("Login user with cookie got user id: " + userId);

            User user = userRepo.GetUserById(userId);
            System.Diagnostics.Debug.WriteLine(user);

            if (user == null)
            {
                return new Response<User>()
                {
                    Success = false,
                    Message = "User with id " + userId + " not found",
                    Payload = null,
                };
            }

            return new Response<User>()
            {
                Success = true,
                Message = "Login by cookie success",
                Payload = user,
            };
        }

    }
}