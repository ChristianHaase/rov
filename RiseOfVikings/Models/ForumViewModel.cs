using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBConnection;
using System.ComponentModel.DataAnnotations;

namespace RiseOfVikings.Models
{
    public class ForumViewModel
    {
        public List<Subforum> AllSubforums { get; set; }
        public Subforum SelectedSubforum { get; set; }
        public List<Thread> Threads { get; set; }
        public Thread SelectedThread { get; set; }
        public User ThreadUser { get; set; }
        public List<Comment> Comments { get; set; }
        public User CommentUser { get; set; }
        public User LoggedInUser { get; set; }
        public List<Thread> News { get; set; }
        public Forum SelectedForum { get; set; }
    }
}