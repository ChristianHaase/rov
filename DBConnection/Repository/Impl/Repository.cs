using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;

namespace DBConnection.Repository.Impl
{
    public class Repository : IRepository
    {

        /// <summary>
        /// Return a given member with the given parametres
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.User.Include("Role").FirstOrDefault(x => x.username == username);
            }
        }

        /// <summary>
        /// Return the user with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.User.Include("Role").FirstOrDefault(x => x.id == id);
            }
        }

        /// <summary>
        /// Creates a new user in the database with the given properties
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(User user)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                user.role_id = 3;
                db.User.Add(user);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Edits an already existing user in the database, with the new given properties
        /// </summary>
        /// <param name="user"></param>
        public void EditUser(User user)
        {
            if (user.battletag == null)
            {
                user.battletag = "Ikke oplyst";
            }
            using (var db = new RiseOfVikingsEntities())
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Returns a list of all the current users in the database
        /// </summary>
        /// <returns></returns>
        public List<User> AllMembers()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.User.Include("Role").ToList();
            }
        }

        /// <summary>
        /// Return a list of all the current roles in the database
        /// </summary>
        /// <returns></returns>
        public List<Role> AllRoles()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.Role.ToList();
            }
        }

        /// <summary>
        /// Changes the role of the desired user, to the desired role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        public void ChangeRole(int userId, int roleId)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var user = db.User.FirstOrDefault(x => x.id == userId);
                user.role_id = roleId;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the given user from the database, applying the reason, and the admin ID of the admin who deleted this user (if any)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adminId"></param>
        /// <param name="message"></param>
        public void DeleteUser(int userId, int? adminId, string message)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var user = db.User.FirstOrDefault(x => x.id == userId);
                if (adminId != null)
                {
                    var deletedUser = new DeletedUsers()
                    {
                        firstname = user.firstname,
                        lastname = user.lastname,
                        email = user.email,
                        username = user.username,
                        reason = message,
                        deleted_date = DateTime.Now,
                        admin = db.User.FirstOrDefault(x => x.id == adminId).username
                    };

                    db.DeletedUsers.Add(deletedUser);
                    db.User.Remove(user);
                    db.SaveChanges();
                }
                else
                {
                    var deletedUser = new DeletedUsers()
                    {
                        firstname = user.firstname,
                        lastname = user.lastname,
                        email = user.email,
                        username = user.username,
                        reason = message,
                        deleted_date = DateTime.Now,
                        admin = null
                    };

                    db.DeletedUsers.Add(deletedUser);
                    db.User.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Returns a list of all the deleted users and their reason for the deletion
        /// </summary>
        /// <returns></returns>
        public List<DeletedUsers> DeletedUsers()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.DeletedUsers.ToList();
            }
        }

        /// <summary>
        /// Creates a new subfoum with the given parametres
        /// </summary>
        /// <param name="subforum"></param>
        public void CreateSubforum(Subforum subforum)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                db.Subforum.Add(subforum);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the subforum with the given ID
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSubforum(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var subforum = db.Subforum.FirstOrDefault(x => x.id == id);
                db.Subforum.Remove(subforum);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the "About" for the alliance
        /// </summary>
        /// <returns></returns>
        public About GetAboutAlliance()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.About.FirstOrDefault(x => x.id == 1);
            }
        }

        /// <summary>
        /// Return the "About" for the horde
        /// </summary>
        /// <returns></returns>
        public About GetAboutHorde()
        {
            using (var db = new RiseOfVikingsEntities())
            {
                return db.About.FirstOrDefault(x => x.id == 2);
            }
        }

        /// <summary>
        /// Change the about for the faction with the given id, with the given "about" string
        /// </summary>
        /// <param name="id"></param>
        /// <param name="about"></param>
        public void ChangeAbout(int id, string about)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var obj = db.About.FirstOrDefault(x => x.id == id);
                obj.about1 = about;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all videos
        /// </summary>
        /// <returns></returns>
        public List<Videos> GetAllVideos()
        {
            using(var db = new RiseOfVikingsEntities()){
                return db.Videos.OrderByDescending(x => x.created_date).ToList();
            }
        }

        /// <summary>
        /// Adds a new video
        /// </summary>
        /// <param name="video"></param>
        public void AddNewVideo(Videos video)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                video.created_date = DateTime.Now;
                db.Videos.Add(video);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the video with the given id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveVideo(int id)
        {
            using (var db = new RiseOfVikingsEntities())
            {
                var vid = db.Videos.FirstOrDefault(x => x.id == id);
                db.Videos.Remove(vid);
                db.SaveChanges();
            }
        }
    }
}
