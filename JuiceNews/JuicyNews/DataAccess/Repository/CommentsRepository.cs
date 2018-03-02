using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentsRepository : BaseRepository<Comment>
    {
        public List<Comment> GetCommentsByNewsId(int? newsId)
        {
            return Items.Where(i => i.NewsId == newsId.Value).ToList();
        }
    }
}
