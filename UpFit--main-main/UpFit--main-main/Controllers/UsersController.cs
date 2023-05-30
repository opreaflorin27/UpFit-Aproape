using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UpFit__main.Models;

namespace UpFit__main.Controllers
{
    public class UsersController : Controller
    {
        private CodeFirstDb db = new CodeFirstDb();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }


        public ActionResult AddDetails()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (CodeFirstDb db = new CodeFirstDb())
            {
                var usr = db.users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    Session["SubscriptionType"] = usr.SubscriptionTypeFK.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password.");
                }
            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"]);

                // Retrieve the user from the database based on the UserID
                User user = db.users.Find(userId);

                if (user != null)
                {
                    return View(user);
                }
            }

            // If the user is not logged in or not found in the database, redirect to the login page
            return RedirectToAction("Login");
        }

        // LoginRegistration
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // UpFit!-main

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ViewAccounts()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(
            [Bind(Include =
                "UserID,SubscriptionTypeFK,UserName,Password,FirstName,LastName,Gender,Age,Height,Weight,KcalDaily,Lifestyle")]
            User user)
        {
            if (user.Gender == "M")
            {
                user.KcalDaily =
                    (int)Math.Round(user.Lifestyle *
                                    (66 + (13.7 * user.Weight) + (5 * user.Height) - (6.8 * user.Age)));
            }
            else
            {
                user.KcalDaily = (int)Math.Round(user.Lifestyle *
                                                 (65 + (9.5 * user.Weight) + (1.8 * user.Height) - (4.7 * user.Age)));
            }

            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("LoggedIn");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include =
                "UserID,SubscriptionTypeFK,UserName,Password,FirstName,LastName,Gender,Age,Height,Weight,KcalDaily")]
            User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult VideoList()
        {
            using (CodeFirstDb db = new CodeFirstDb())
            {
                List<Video> Videolist = new List<Video>();
                foreach (Video video in db.videos)
                {
                    Video dbVideo = new Video();
                    {
                        dbVideo.Vname = video.Vname;
                        dbVideo.Vpath = video.Vpath.ToString();
                    }
                    ;
                    Videolist.Add(dbVideo);
                }

                return View(Videolist);
            }
        }
    }
}
