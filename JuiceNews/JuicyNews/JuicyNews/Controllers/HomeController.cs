using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using JuicyNews.ViewModels.Home;
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

        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            newsRepository = RepositoryFactory.GetNewsRepository();

            model.LatestNews = newsRepository.GetAll().OrderByDescending(n => n.DateOfPublishing).Take(3).ToList();
            model.News = newsRepository.GetAll().OrderByDescending(n => n.DateOfPublishing).Skip(3).ToList();

            return View(model);
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