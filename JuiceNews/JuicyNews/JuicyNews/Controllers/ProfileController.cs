using DataAccess.Repository;
using JuicyNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuicyNews.Controllers
{
    public class ProfileController : Controller
    {
        private UsersRepository userRepository;
        // GET: Profile
        public ActionResult Index()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");

            userRepository = RepositoryFactory.GetUsersRepository();
            ViewData["userProfile"] = userRepository.GetById(AuthenticationManager.LoggedUser.Id);

            return View();
        }
    }
}