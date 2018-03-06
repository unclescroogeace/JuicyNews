using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.News
{
    public class NewsIndexViewModel
    {
        public List<DataAccess.Entity.News> News { get; set; }
    }
}