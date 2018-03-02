using DataAccess.Entity;
using DataAccess.Repository;
using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.Models
{
    public class AuthenticationManager
    {
        public static User LoggedUser
        {
            get
            {
                AuthenticationService authenticationService = null;

                if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                    HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();

                authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
                return authenticationService.LoggedUser;
            }
        }

        public static void Authenticate(string username, string password)
        {
            AuthenticationService authenticationService = null;

            if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();

            authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
            authenticationService.AuthenticateUser(username, password);
        }

        public static void Logout()
        {
            ChangeUserStatus(LoggedUser.Id, false);
            HttpContext.Current.Session["LoggedUser"] = null;
        }

        public static void ChangeUserStatus(int id, bool status)
        {
            UsersRepository usersRepository = RepositoryFactory.GetUsersRepository();

            User user = usersRepository.GetById(id);

            user.Status = status;

            usersRepository.Save(user);
        }
    }
}