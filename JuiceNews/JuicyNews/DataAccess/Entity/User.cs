using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
