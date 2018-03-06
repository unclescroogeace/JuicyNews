using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using JuicyNews.ViewModels.News;
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

            newsRepository = RepositoryFactory.GetNewsRepository();
            
            NewsIndexViewModel model = new NewsIndexViewModel();
            model.News = newsRepository.GetAll();

            return View(model);
        }

        public ActionResult CreateNews()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");

            return View();
        }
        
        [HttpPost]
        public ActionResult CreateNews(NewsCreateViewModel model)
        {
            model.DateOfPublishing = DateTime.Now;
            model.UserId = AuthenticationManager.LoggedUser.Id;
            model.Author = AuthenticationManager.LoggedUser.FirstName + " " + AuthenticationManager.LoggedUser.LastName;

            newsRepository = RepositoryFactory.GetNewsRepository();
            News news = new News();
            
            news.Author = model.Author;
            news.Content = model.Content;
            news.DateOfPublishing = model.DateOfPublishing;
            news.Title = model.Title;
            news.UserId = model.UserId;

            newsRepository.Save(news);
            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public ActionResult EditNews(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Index", "Home");
            
            newsRepository = RepositoryFactory.GetNewsRepository();

            News news = newsRepository.GetById(id);

            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }

            NewsEditViewModel model = new NewsEditViewModel();

            model.Author = news.Author;
            model.Content = news.Content;
            model.DateOfPublishing = news.DateOfPublishing;
            model.Id = news.Id;
            model.Title = news.Title;
            model.UserId = news.UserId;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditNews(NewsEditViewModel model)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            newsRepository = RepositoryFactory.GetNewsRepository();

            News news = newsRepository.GetById(model.Id);

            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }

            news.Id = model.Id;
            news.Author = model.Author;
            news.Content = model.Content;
            news.DateOfPublishing = model.DateOfPublishing;
            news.Title = model.Title;
            news.UserId = model.UserId;

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

        public ActionResult ViewNews(int id)
        {
            newsRepository = RepositoryFactory.GetNewsRepository();
            CommentsRepository commentsRepository = RepositoryFactory.GetCommentsRepository();

            News news = newsRepository.GetById(id);

            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }

            NewsViewNewsViewModel model = new NewsViewNewsViewModel();
            
            model.News = news;
            model.Comments = commentsRepository.GetCommentsByNewsId(id);

            ViewData["loggedUser"] = AuthenticationManager.LoggedUser;

            return View(model);
        }
    }
}