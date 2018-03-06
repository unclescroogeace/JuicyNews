using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<DataAccess.Entity.News> LatestNews { get; set; }
        public List<DataAccess.Entity.News> News { get; set; }
    }
}