using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class NewsRepository : BaseRepository<News>
    {
        public List<News> GetAll()
        {
            return Items.ToList();
        }
    }
}
