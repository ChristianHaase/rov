using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBConnection;
using Microsoft.Ajax.Utilities;
using RiseOfVikings.Models;

namespace RiseOfVikings.Controllers
{
    public class AllianceController : Controller
    {
        private Facade _facade = new Facade();

        // GET: Alliance
        public ActionResult Index()
        {
            var model = new ForumViewModel()
            {
                News = _facade.GetForumRepo().AllThreadsForSubforum(3)
            };
            return View(model);
        }

        public ActionResult About()
        {
            var model = new UserViewModel()
            {
                LoggedInUser = Session["User"] as User,
                About = _facade.GetRepo().GetAboutAlliance()
            };
            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Videos()
        {
            var model = new VideoViewModel();

            if(Session["User"] != null){
                model.LoggedInUser = Session["User"] as User;
                model.AllVideos = _facade.GetRepo().GetAllVideos();
            }
            else
            {
                model.AllVideos = _facade.GetRepo().GetAllVideos();
            }

            return View(model);
        }

        public ActionResult AddNewVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewVideoConfirmed(Videos video)
        {
            _facade.GetRepo().AddNewVideo(video);

            return RedirectToAction("Videos", "Alliance");
        }

        public ActionResult RemoveVideo( int id)
        {
            _facade.GetRepo().RemoveVideo(id);

            return RedirectToAction("Videos", "Alliance");
        }

        public ActionResult Forum()
        {
            if (Session["User"] != null)
            {
                var model = new ForumViewModel()
                {
                    AllSubforums = _facade.GetForumRepo().AllSubforumsForForum(1),
                    LoggedInUser = Session["User"] as User
                };
                return View(model);

            }
            var model2 = new ForumViewModel()
            {
                AllSubforums = _facade.GetForumRepo().AllSubforumsForForum(1),
                LoggedInUser = null
            };
            return View(model2);
        }

        public ActionResult RaidTeams()
        {
            return View();
        }

        public ActionResult Streamers()
        {
            return View();
        }

        public ActionResult Members()
        {
            var model = new MemberViewModel()
            {
                AllMembers = _facade.GetRepo().AllMembers()
            };
            return View(model);
        }

        public ActionResult Search(string searchString)
        {
            var model = new MemberViewModel()
            {
                AllMembers =
                    _facade.GetRepo()
                        .AllMembers()
                        .Where(
                            x =>
                                //Username search
                                x.username.ToLower().Contains(searchString.ToLower()) ||
                                //Rank search
                                x.Role.role_name.ToLower().Contains(searchString.ToLower()))
                        .ToList()
            };

            return View("Members", model);
        }

        public ActionResult SubforumIndex(int id)
        {
            var model = new ForumViewModel()
            {
                Threads = _facade.GetForumRepo().AllThreadsForSubforum(id),
                SelectedSubforum = _facade.GetForumRepo().Subforum(id),
                LoggedInUser = Session["User"] as User
            };

            return View(model);
        }

        public ActionResult ThreadIndex(int id, int subforumId)
        {
            var model = new ForumViewModel()
            {
                SelectedThread = _facade.GetForumRepo().Thread(id),
                ThreadUser = _facade.GetForumRepo().Thread(id).User,
                LoggedInUser = Session["User"] as User,
                Comments = _facade.GetForumRepo().AllCommentsForThread(id),
                SelectedSubforum = _facade.GetForumRepo().Subforum(subforumId)
            };
            return View(model);
        }

        public ActionResult CreateComment(string stringComment, int userId, int threadId, int subforumId)
        {
            if (stringComment == "")
            {
                Session["CommentError"] = "Du kan ikke oprette en kommentar medmindre feltet er udfyldt";
                return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforumId });
            }
            var comment = new Comment()
            {
                body = stringComment,
                user_id = userId,
                thread_id = threadId,
                created_date = DateTime.Now
            };
            _facade.GetForumRepo().CreateComment(comment);
            return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforumId });
        }

        public ActionResult RemoveWithMessageConfirm(int commentId, int threadId, int subforum)
        {
            _facade.GetForumRepo().DeleteCommentWithMessage(commentId);
            return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforum });
        }

        public ActionResult DeleteComment(int commentId, int threadId, int subforum)
        {
            _facade.GetForumRepo().DeleteComment(commentId);

            return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforum });
        }

        public ActionResult CreateThread(int subforumId)
        {
            var model = new ForumViewModel()
            {
                LoggedInUser = Session["User"] as User,
                SelectedSubforum = _facade.GetForumRepo().Subforum(subforumId),

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateThreadConfirmed(Thread thread)
        {
            if (thread.body == null || thread.headline == null)
            {
                return RedirectToAction("SubforumIndex", new { id = thread.subforum_id });
            }
            thread.created_date = DateTime.Now;
            _facade.GetForumRepo().CreateThread(thread);
            return RedirectToAction("SubforumIndex", new { id = thread.subforum_id });
        }

        public ActionResult DeleteThread(int id, int subforumId)
        {
            _facade.GetForumRepo().DeleteThread(id);

            return RedirectToAction("SubforumIndex", new { id = subforumId });
        }

        public ActionResult SetSticky(int threadId, int subforum)
        {
            _facade.GetForumRepo().SetSticky(threadId);

            return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforum });
        }

        public ActionResult SetReadOnly(int threadId, int subforum)
        {
            _facade.GetForumRepo().SetReadOnly(threadId);

            return RedirectToAction("ThreadIndex", new { id = threadId, subforumId = subforum });
        }

        public ActionResult DeletedUsers()
        {
            var model = new UserViewModel()
            {
                DeletedUsers = _facade.GetRepo().DeletedUsers()
            };

            return View(model);
        }

        public ActionResult CreateSubforum()
        {
            var model = new ForumViewModel()
            {
                SelectedForum = _facade.GetForumRepo().AllForums().FirstOrDefault(x => x.id == 1)
            };
            return View(model);
        }

        public ActionResult CreateSubforumConfirmed(Subforum subforum, int forum)
        {
            if (subforum.description == null || subforum.subforum_name == null)
            {
                Session["CreateSubforumError"] = "Du bedes udfylde begge felter";
                return RedirectToAction("CreateSubforum", "Alliance");
            }
            if (forum == 1)
            {
                _facade.GetForumRepo().CreateSubforumAlliance(subforum);
                Session["CreateSubforumError"] = null;
            }
            else
            {
                _facade.GetForumRepo().CreateSubforumBoth(subforum);
                Session["CreateSubforumError"] = null;
            }
            return RedirectToAction("Forum", "Alliance");
        }

        public ActionResult DeleteSubforum(int id)
        {
            _facade.GetForumRepo().DeleteSubforum(id);
            return RedirectToAction("Forum", "Alliance");
        }

        public ActionResult ChangeAbout()
        {
            var model = new UserViewModel()
            {
                About = _facade.GetRepo().GetAboutHorde()
            };

            return View(model);
        }

        public ActionResult ChangeAboutConfirmed(int id, string about)
        {
            _facade.GetRepo().ChangeAbout(id, about);
            return RedirectToAction("About", "Alliance");
        }
    }
}