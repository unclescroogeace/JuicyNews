using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuicyNews.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository newsRepository;
        // GET: News
        public ActionResult Index()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");

            UsersRepository userRepo = RepositoryFactory.GetUsersRepository();
            newsRepository = RepositoryFactory.GetNewsRepository();

            ViewData["news"] = newsRepository.GetAll();
            ViewData["users"] = userRepo.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult CreateNews()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateNews(News news)
        {
            news.DateOfPublishing = DateTime.Today;
            news.DateOfLastEdit = DateTime.Today;
            news.UserId = AuthenticationManager.LoggedUser.Id;
            newsRepository = RepositoryFactory.GetNewsRepository();
            newsRepository.Save(news);
            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public ActionResult EditNews(int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");


            newsRepository = RepositoryFactory.GetNewsRepository();

            News news = null;
            if (id == null)
                news = new News();
            else
                news = newsRepository.GetById(id.Value);

            ViewData["editNews"] = news;
            
            return View();
        }

        [HttpPost]
        public ActionResult EditNews(News news)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            news.DateOfLastEdit = DateTime.Today;
            newsRepository = RepositoryFactory.GetNewsRepository();

            newsRepository.Save(news);

            return RedirectToAction("Index", "News");
        }

        public ActionResult DeleteNews(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            newsRepository = RepositoryFactory.GetNewsRepository();
            News news = newsRepository.GetById(id);
            newsRepository.Delete(news);

            return RedirectToAction("Index", "News");
        }

        public ActionResult ViewNews(int? id)
        {
            newsRepository = RepositoryFactory.GetNewsRepository();
            CommentsRepository commentsRepository = RepositoryFactory.GetCommentsRepository();
            News news = null;
            if (id == null)
                return RedirectToAction("Index", "Home");
            else
                news = newsRepository.GetById(id.Value);

            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Comment> newsComments = new List<Comment>();
            newsComments = commentsRepository.GetCommentsByNewsId(id.Value);
            UsersRepository usersRepository = RepositoryFactory.GetUsersRepository();
            User author = usersRepository.GetById(news.UserId);

            if (author == null)
            {
                ViewData["authorFullName"] = "Unknown";
            }
            else
            {
                ViewData["authorFullName"] = author.FirstName + " " + author.LastName;
            }

            ViewData["newsComments"] = newsComments;
            ViewData["viewNews"] = news;
            ViewData["loggedUser"] = AuthenticationManager.LoggedUser;
            ViewData["users"] = usersRepository.GetAll();

            return View();
        }
    }
}