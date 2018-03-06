using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UsersRepository : BaseRepository<User>
    {
        public User GetByUsernameAndPassword(string username, string password)
        {
            return Items.Where(i => i.Username == username && i.Password == password).FirstOrDefault();
        }

        public User FullNameById(int id)
        {
            return (User)Items.Where(i => id == i.Id);
        }

        public void ChangeUserStatus(User user, bool status)
        {
            
        }
    }
}
