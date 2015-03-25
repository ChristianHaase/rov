using DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiseOfVikings.Models
{
    public class VideoViewModel
    {
        public List<Videos> AllVideos { get; set; }
        public User LoggedInUser { get; set; }
    }
}