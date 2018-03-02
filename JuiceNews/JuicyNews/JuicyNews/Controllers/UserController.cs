using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
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
            ViewData["users"] = userRepository.GetAll();

            return View();
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (AuthenticationManager.LoggedUser == null || !(AuthenticationManager.LoggedUser.Id == id.Value || AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Index", "Home");


            userRepository = RepositoryFactory.GetUsersRepository();

            ViewData["loggedUser"] = AuthenticationManager.LoggedUser;

            User user = null;
            if (id == null)
                user = new User();
            else
                user = userRepository.GetById(id.Value);

            ViewData["editUser"] = user;

            return View();
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (AuthenticationManager.LoggedUser == null && !(AuthenticationManager.LoggedUser.Id == user.Id || AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Login", "Home");
            
            userRepository = RepositoryFactory.GetUsersRepository();

            if(Request.Form["NewPassword"] != "")
            {
                user.Password = Utility.PasswordHashing.md5Hashing(Request.Form["NewPassword"]);
            }

            userRepository.Save(user);

            if (AuthenticationManager.LoggedUser.Id == user.Id)
            {
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