//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBConnection
{
    using System;
    using System.Collections.Generic;
    
    public partial class Thread
    {
        public Thread()
        {
            this.Comment = new HashSet<Comment>();
        }
    
        public int id { get; set; }
        public string headline { get; set; }
        public string body { get; set; }
        public System.DateTime created_date { get; set; }
        public int user_id { get; set; }
        public bool sticky { get; set; }
        public bool read_only { get; set; }
        public int subforum_id { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Subforum Subforum { get; set; }
        public virtual User User { get; set; }
    }
}