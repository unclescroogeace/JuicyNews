using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using JuicyNews.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuicyNews.Controllers
{
    public class UserController : Controller
    {
        private UsersRepository userRepository;
        
        // GET: User
        public ActionResult Index()
        {
            if (AuthenticationManager.LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!AuthenticationManager.LoggedUser.IsAdministrator)
            {
                return RedirectToAction("Index", "Home"); 
            }

            userRepository = RepositoryFactory.GetUsersRepository();

            UserIndexViewModel model = new UserIndexViewModel();
            model.Users = userRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            if (AuthenticationManager.LoggedUser == null || !(AuthenticationManager.LoggedUser.Id == id || AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Index", "Home");
            
            userRepository = RepositoryFactory.GetUsersRepository();

            ViewData["loggedUser"] = AuthenticationManager.LoggedUser;

            User user = new User();
            user = userRepository.GetById(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            UserEditUserViewModel model = new UserEditUserViewModel();

            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.Id = user.Id;
            model.IsAdministrator = user.IsAdministrator;
            model.LastName = user.LastName;
            model.Password = user.Password;
            model.RegistrationDate = user.RegistrationDate;
            model.Status = user.Status;
            model.Username = user.Username;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserEditUserViewModel model)
        {
            if (AuthenticationManager.LoggedUser == null && !(AuthenticationManager.LoggedUser.Id == model.Id || AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Login", "Home");
            
            userRepository = RepositoryFactory.GetUsersRepository();

            if(Request.Form["NewPassword"] != "")
            {
                model.Password = Utility.PasswordHashing.md5Hashing(Request.Form["NewPassword"]);
            }

            User user = new User();

            user.Id = model.Id;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.IsAdministrator = model.IsAdministrator;
            user.LastName = model.LastName;
            user.Password = model.Password;
            user.RegistrationDate = model.RegistrationDate;
            user.Status = model.Status;
            user.Username = model.Username;

            userRepository.Save(user);

            if (AuthenticationManager.LoggedUser.Id == user.Id)
            {
                AuthenticationManager.LoggedUser.FirstName = user.FirstName;
                AuthenticationManager.LoggedUser.LastName = user.LastName;
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        public ActionResult DeleteUser(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            userRepository = RepositoryFactory.GetUsersRepository();
            User user = userRepository.GetById(id);
            userRepository.Delete(user);

            return RedirectToAction("Index", "User");
        }
    }
}