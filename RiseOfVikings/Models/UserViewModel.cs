using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBConnection;

namespace RiseOfVikings.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public User LoggedInUser { get; set; }
        public List<Role> AllRoles { get; set; }
        public List<DeletedUsers> DeletedUsers { get; set; }
        public About About { get; set; }
    }
}