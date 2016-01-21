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
            TempData["Message"] = "Signup successfully recorded. Can't wait to check you into the boards!!";
            return RedirectToAction("Index");
        }

    }
}
