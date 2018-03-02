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
    public class HomeController : Controller
    {
        private NewsRepository newsRepository;
        private UsersRepository usersRepository;
        public ActionResult Index()
        {
            newsRepository = RepositoryFactory.GetNewsRepository();
            usersRepository = RepositoryFactory.GetUsersRepository();

            List<News> lastNews = newsRepository.GetAll().OrderByDescending(n => n.DateOfPublishing).Take(3).ToList();

            ViewData["lastNews"] = lastNews;
            ViewData["allNews"] = newsRepository.GetAll().OrderByDescending(n => n.DateOfPublishing).ToList();
            ViewData["users"] = usersRepository.GetAll();

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            password = Utility.PasswordHashing.md5Hashing(password);
            AuthenticationManager.Authenticate(username, password);

            if (AuthenticationManager.LoggedUser == null)
            {
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
                ViewData["username"] = username;

                return View();
            }

            AuthenticationManager.ChangeUserStatus(AuthenticationManager.LoggedUser.Id, true);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            AuthenticationManager.Logout();

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User item)
        {
            item.Password = Utility.PasswordHashing.md5Hashing(item.Password);
            item.Status = false;
            item.IsAdministrator = false;
            item.RegistrationDate = DateTime.Now;
            UsersRepository repo = RepositoryFactory.GetUsersRepository();
            repo.Save(item);

            return RedirectToAction("Index", "Home");
        }
    }
}