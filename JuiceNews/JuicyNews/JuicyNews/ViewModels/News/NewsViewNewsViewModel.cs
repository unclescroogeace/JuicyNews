using JuicyNews.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.News
{
    public class NewsViewNewsViewModel
    {
        public DataAccess.Entity.News News { get; set; }

        public List<DataAccess.Entity.Comment> Comments { get; set; }

        public CommentViewNewsViewModel CommentViewModel { get; set; }
    }
}