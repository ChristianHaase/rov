using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Repository.Impl
{
    class ForumRepository : IForumRepository
    {
        public List<Forum> AllForums()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Forum.ToList();
            }
        }

        public List<Subforum> AllSubforumsForForum(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Subforum.Include("Thread").Where(x => x.forum_id == id).ToList();
            }
        }

        public Subforum Subforum(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Subforum.FirstOrDefault(x => x.id == id);
            }
        }

        public List<Thread> AllThreadsForSubforum(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Thread.Include("User").Where(x => x.subforum_id == id).OrderByDescending(x => x.created_date).ToList();
            }
        }

        public Thread Thread(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Thread.Include("User").Include("User.Role").FirstOrDefault(x => x.id == id);
            }
        }

        public List<Comment> AllCommentsForThread(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Comment.Include("User").Include("User.Role").Where(x => x.thread_id == id).ToList();
            }
        }

        public void CreateSubforumHorde(Subforum subforum)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                subforum.forum_id = 2;
                db.Subforum.Add(subforum);
                db.SaveChanges();
            }
        }

        public void CreateSubforumAlliance(Subforum subforum)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                subforum.forum_id = 1;
                db.Subforum.Add(subforum);
                db.SaveChanges();
            }
        }

        public void CreateSubforumBoth(Subforum subforum)
        {
            if (subforum.description == null)
            {
                return;
            }
            using (var db = new RiseOfVikingsEntities())
            {
                subforum.forum_id = 1;
                db.Subforum.Add(subforum);
                db.SaveChanges();
                subforum.forum_id = 2;
                db.Subforum.Add(subforum);
                db.SaveChanges();
            }
        }

        public void CreateThread(Thread thread)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Thread.Add(thread);
                db.SaveChanges();
            }
        }

        public void CreateComment(Comment comment)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Comment.Add(comment);
                db.SaveChanges();
            }
        }

        public void DeleteSubforum(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Subforum.Remove(db.Subforum.FirstOrDefault(x => x.id == id));
                db.SaveChanges();
            }
        }

        public void DeleteThread(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Thread.Remove(db.Thread.FirstOrDefault(x => x.id == id));
                db.SaveChanges();
            }
        }

        public void DeleteComment(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Comment.Remove(db.Comment.FirstOrDefault(x => x.id == id));
                db.SaveChanges();
            }
        }

        public void DeleteCommentWithMessage(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var comment = db.Comment.FirstOrDefault(x => x.id == id);
                comment.body = "(Fjernet på grund af upassende indhold)";
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SetSticky(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var thread = db.Thread.FirstOrDefault(x => x.id == id);
                thread.sticky = true;
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SetReadOnly(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var thread = db.Thread.FirstOrDefault(x => x.id == id);
                thread.read_only = true;
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
