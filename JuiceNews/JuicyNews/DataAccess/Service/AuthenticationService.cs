using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepo = RepositoryFactory.GetUsersRepository();
            LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
