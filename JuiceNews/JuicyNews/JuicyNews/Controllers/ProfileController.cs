using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using JuicyNews.ViewModels.Profile;
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

            User user = new User();
            user = userRepository.GetById(AuthenticationManager.LoggedUser.Id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProfileIndexViewModel model = new ProfileIndexViewModel();

            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.Id = user.Id;
            model.IsAdministrator = user.IsAdministrator;
            model.LastName = user.LastName;
            model.RegistrationDate = user.RegistrationDate;
            model.Status = user.Status;
            model.Username = user.Username;

            return View(model);
        }
    }
}