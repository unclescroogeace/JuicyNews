using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RepositoryFactory
    {
        public static UsersRepository GetUsersRepository()
        {
            return new UsersRepository();
        }

        public static NewsRepository GetNewsRepository()
        {
            return new NewsRepository();
        }

        public static CommentsRepository GetCommentsRepository()
        {
            return new CommentsRepository();
        }
    }
}
