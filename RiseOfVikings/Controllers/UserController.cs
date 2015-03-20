using System;
using System.Net;
using System.Web.Mvc;
using DBConnection;
using DevOne.Security.Cryptography.BCrypt;
using RiseOfVikings.Models;

namespace RiseOfVikings.Controllers
{
    public class UserController : Controller
    {
        readonly Facade _facade = new Facade();

        //GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: User/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var model = new UserViewModel();
            try
            {
                var user = _facade.GetRepo().GetUser(username);
                if (BCryptHelper.CheckPassword(password, user.password))
                {
                    model.User = user;
                    Session["User"] = user;
                    Session["Username"] = user.username;
                    Session["Role"] = user.role_id;
                    Session["LoginErrorMessage"] = null;

                    return RedirectToAction("UserProfile");
                }
                else
                {
                    Session["LoginErrorMessage"] = "Brugernavn og kode passer ikke sammen. Prøv venligst igen.";
                }
            }
            catch
            {
                Session["LoginErrorMessage"] = "Brugernavn findes ikke. Prøv venligst igen.";
            }

            return RedirectToAction("Login", "User");
        }

        //GET: User/Logout
        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index", "Home");
        }



        // GET: User/Details
        public ActionResult Details(int id)
        {
            var model = new UserViewModel()
            {
                User = _facade.GetRepo().GetUser(id),
                LoggedInUser = Session["User"] as User,
                AllRoles = _facade.GetRepo().AllRoles()
            };
            return View(model);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.member_since = DateTime.Now;
            if (user.firstname == null || user.lastname == null || user.username == null || user.password == null || user.email == null)
            {
                Session["CreateError"] = "Udfyld venligst alle felter markeret med *";
                return RedirectToAction("Create", "User");
            }
            if (user.battletag == null)
            {
                user.battletag = "Ikke oplyst";
            }
            var nonHashetPass = user.password;
            user.password = BCryptHelper.HashPassword(nonHashetPass, BCryptHelper.GenerateSalt(5));
            _facade.GetRepo().CreateUser(user);
            Session["User"] = user;
            Session["username"] = user.username;
            return RedirectToAction("Index", "Home");
        }

        //GET: User/Userprofile
        public ActionResult UserProfile()
        {
            var user = Session["User"] as User;
            var model = new UserViewModel
            {
                User = user,
                LoggedInUser = Session["User"] as User
            };
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        //GET: User/Edit
        public ActionResult Edit()
        {
            var model = new UserViewModel { User = Session["User"] as User };
            return View(model);
        }

        //POST: User/Edit
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (user.firstname == null || user.lastname == null || user.email == null || user.username == null)
            {
                Session["EditError"] = "Du bedes udfylde alle felter bortset fra battletag";
                return RedirectToAction("Edit", "User");
            }
            else
            {
                var tempUser = _facade.GetRepo().GetUser(user.username);
                user.password = tempUser.password;
                user.member_since = tempUser.member_since;
                user.role_id = tempUser.role_id;
                _facade.GetRepo().EditUser(user);
                Session["User"] = user;
                Session["Username"] = user.username;
                Session["EditError"] = null;
            }

            return RedirectToAction("Index", "Home");
        }

        //GET: User/ChangeRole
        public ActionResult ChangeRole(int userId, int roleId)
        {
            _facade.GetRepo().ChangeRole(userId, roleId);
            return RedirectToAction("Details", new { id = userId });
        }

        public ActionResult DeleteUser(int userId)
        {
            var model = new UserViewModel()
            {
                User = _facade.GetRepo().GetUser(userId),
                LoggedInUser = Session["User"] as User
            };
            return View(model);
        }

        //GET: User/DeleteUserConfirmed
        public ActionResult DeleteUserConfirmed(int userId, int? adminId, string message)
        {
            _facade.GetRepo().DeleteUser(userId, adminId, message);
            var user = Session["User"] as User;

            if (user.id == userId)
            {
                Session["User"] = null;
                Session["Username"] = null;
                Session["Role"] = null;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
