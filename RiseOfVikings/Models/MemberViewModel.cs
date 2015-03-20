using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBConnection;

namespace RiseOfVikings.Models
{
    public class MemberViewModel
    {
        public List<User> AllMembers { get; set; }
        public User SelectedMember { get; set; }
    }
}