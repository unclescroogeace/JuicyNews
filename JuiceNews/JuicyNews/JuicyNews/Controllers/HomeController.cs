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
            LoginViewModel model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            model.Password = Utility.PasswordHashing.md5Hashing(model.Password);
            AuthenticationManager.Authenticate(model.Username, model.Password);

            if (!ModelState.IsValid)
            {
                model.Password = "";
                return View(model);
            }

            if (AuthenticationManager.LoggedUser == null)
            {
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
                model.Password = "";
                return View(model);
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
            RegisterViewModel model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Password = "";
                return View(model);
            }

            User user = new User();

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.IsAdministrator = false;
            user.LastName = model.LastName;
            user.Password = Utility.PasswordHashing.md5Hashing(model.Password);
            user.RegistrationDate = DateTime.Now;
            user.Status = false;
            user.Username = model.Username;

            UsersRepository repo = RepositoryFactory.GetUsersRepository();
            repo.Save(user);

            return RedirectToAction("Index", "Home");
        }
    }
}