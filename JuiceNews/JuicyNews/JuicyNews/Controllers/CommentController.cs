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
    public class CommentController : Controller
    {
        // GET: Comment
        private CommentsRepository commentsRepository;

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            commentsRepository = RepositoryFactory.GetCommentsRepository();
            commentsRepository.Save(comment);

            return RedirectToAction("ViewNews", "News", new { id = comment.NewsId });
        }

        [HttpGet]
        public ActionResult EditComment(int? commentId, int? newsId)
        {
            commentsRepository = RepositoryFactory.GetCommentsRepository();
            NewsRepository newsRepository = RepositoryFactory.GetNewsRepository();
            Comment editComment;
            if (commentId == null || newsId == null)
                return RedirectToAction("Index", "Home");
            else
            {
                editComment = commentsRepository.GetById(commentId.Value);
            }

            if (editComment == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (AuthenticationManager.LoggedUser == null && ((editComment.UserId != AuthenticationManager.LoggedUser.Id) || !AuthenticationManager.LoggedUser.IsAdministrator))
                return RedirectToAction("Login", "Home");

            ViewData["newsTitle"] = newsRepository.GetById(newsId.Value).Title;
            ViewData["editComment"] = editComment;

            return View();
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