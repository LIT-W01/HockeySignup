using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HockeySignup.Data;
using HockeySignup.Web.Models;

namespace HockeySignup.Web.Controllers
{
    public class HockeyController : Controller
    {
        private string _connectionString =
            @"Data Source=.\sqlexpress;Initial Catalog=HockeySignup;Integrated Security=True";
        public ActionResult Index()
        {
            HomePageViewModel vm = new HomePageViewModel();
            if (TempData["Message"] != null)
            {
                vm.Message = (string)TempData["Message"];
            }
            return View(vm);
        }

        public ActionResult Latest()
        {
            SignupViewModel vm = new SignupViewModel();
            var db = new HockeySignupDb(_connectionString);
            Event e = db.GetLatestEvent();
            EventStatus status = db.GetEventStatus(e);
            vm.Event = e;
            vm.Status = status;
            vm.Signup = new EventSignup();
            if (Request.Cookies["firstName"] != null)
            {
                vm.Signup.FirstName = Request.Cookies["firstName"].Value;
                vm.Signup.LastName = Request.Cookies["lastName"].Value;
                vm.Signup.Email = Request.Cookies["email"].Value;
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Signup(string firstName, string lastName, string email, int eventId)
        {
            var db = new HockeySignupDb(_connectionString);
            Event e = db.GetEventById(eventId);
            var status = db.GetEventStatus(e);
            if (status != EventStatus.Open)
            {
                return RedirectToAction("Index");
            }

            EventSignup es = new EventSignup
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                EventId = eventId
            };
            db.AddEventSignup(es);

            HttpCookie firstNameCookie = new HttpCookie("firstName", firstName); 
            HttpCookie lastNameCookie = new HttpCookie("lastName", lastName); 
            HttpCookie emailCookie = new HttpCookie("email", email); 

            Response.Cookies.Add(firstNameCookie);
            Response.Cookies.Add(lastNameCookie);
            Response.Cookies.Add(emailCookie);

            TempData["Message"] = "Signup successfully recorded. Can't wait to check you into the boards!!";
            return RedirectToAction("Index");
        }

        public ActionResult NotificationSignup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NotificationSignup(string firstName, string lastName, string email)
        {
            var db = new HockeySignupDb(_connectionString);
            NotificationSignup signup = new NotificationSignup
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            db.AddNotificationSignup(signup);
            return View("NotificationConfirmation");
        }

    }
}
