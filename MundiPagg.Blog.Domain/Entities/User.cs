using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.Blog.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
