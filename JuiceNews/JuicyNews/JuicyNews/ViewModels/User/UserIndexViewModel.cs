using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.User
{
    public class UserIndexViewModel
    {
        public List<DataAccess.Entity.User> Users { get; set; }
    }
}