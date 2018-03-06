using DataAccess.Entity;
using DataAccess.Repository;
using JuicyNews.Models;
using JuicyNews.ViewModels.News;
using JuicyNews.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuicyNews.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        private CommentsRepository commentsRepository;

        [HttpPost]
        public ActionResult CreateComment(NewsViewNewsViewModel model)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            model.CommentViewModel.Date = DateTime.Now;
            model.CommentViewModel.Author = AuthenticationManager.LoggedUser.FirstName + " " + AuthenticationManager.LoggedUser.LastName;

            commentsRepository = RepositoryFactory.GetCommentsRepository();
            Comment comment = new Comment();

            comment.Author = model.CommentViewModel.Author;
            comment.Content = model.CommentViewModel.Content;
            comment.Date = model.CommentViewModel.Date;
            comment.NewsId = model.CommentViewModel.NewsId;
            comment.UserId = model.CommentViewModel.UserId;

            commentsRepository.Save(comment);

            return RedirectToAction("ViewNews", "News", new { id = comment.NewsId });
        }

        [HttpGet]
        public ActionResult EditComment(int commentId, int newsId)
        {
            commentsRepository = RepositoryFactory.GetCommentsRepository();
            NewsRepository newsRepository = RepositoryFactory.GetNewsRepository();

            Comment comment = new Comment();
            comment = commentsRepository.GetById(commentId);

            News news = new News();
            news = newsRepository.GetById(newsId);

            if (news == null || comment == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (AuthenticationManager.LoggedUser == null && ((comment.UserId != AuthenticationManager.LoggedUser.Id) || !AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Login", "Home");


            CommentEditCommentViewModel model = new CommentEditCommentViewModel();

            model.Id = comment.Id;
            model.Author = comment.Author;
            model.Content = comment.Content;
            model.Date = comment.Date;
            model.NewsId = comment.NewsId;
            model.UserId = comment.UserId;

            ViewData["newsTitle"] = news.Title;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditComment(Comment comment)
        {
            if (AuthenticationManager.LoggedUser == null && ((comment.UserId != AuthenticationManager.LoggedUser.Id) || !AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Login", "Home");

            commentsRepository = RepositoryFactory.GetCommentsRepository();
            commentsRepository.Save(comment);

            return RedirectToAction("ViewNews", "News", new { id = comment.NewsId });
        }

        public ActionResult DeleteComment(int id)
        {
            if (AuthenticationManager.LoggedUser == null && !AuthenticationManager.LoggedUser.IsAdministrator)
                return RedirectToAction("Login", "Home");
            
            commentsRepository = RepositoryFactory.GetCommentsRepository();
            Comment comment = commentsRepository.GetById(id);
            commentsRepository.Delete(comment);

            return RedirectToAction("ViewNews", "News", new { id = comment.NewsId });
        }
    }
}