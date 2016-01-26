using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HockeySignup.Data;
using HockeySignup.Web.Models;

namespace HockeySignup.Web.Controllers
{
    public class AdminController : Controller
    {
        private string _connectionString =
            @"Data Source=.\sqlexpress;Initial Catalog=HockeySignup;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(DateTime date, int maxPeople, bool sendEmail)
        {
            var db = new HockeySignupDb(_connectionString);
            Event e = new Event
            {
                Date = date,
                MaxPeople = maxPeople
            };
            db.AddEvent(e);
            
            TempData["Message"] = "Event created successfuly, Id: " + e.Id;
            return RedirectToAction("Index", "Hockey");
        }

        private void SendNotificationEmail(string email)
        {
            //baisyaakovgirl123@gmail.com
            //bygirl123

            var fromAddress = new MailAddress("baisyaakovgirl123@gmail.com", "Aidel Maidel");
            var toAddress = new MailAddress(email, "Hockey Player");
            const string fromPassword = "bygirl123";
            const string subject = "Hockey Event Posted";
            string body =
                "Hey Avrumi Friedman<br><br>Good news! This weeks game has been posted! <a href='http://localhost:49279/hockey/latest'>Click Here</a> to sign up!";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public ActionResult History()
        {
            var db = new HockeySignupDb(_connectionString);
            IEnumerable<EventWithCount> events = db.GetEventsWithCounts();

            return View(events);
        }

        public ActionResult EventHistory(int id)
        {
            var db = new HockeySignupDb(_connectionString);
            var e = db.GetEventById(id);
            IEnumerable<EventSignup> signups = db.GetEventSignups(id);
            var vm = new HistoryViewModel { Signups = signups, Event = e };
            return View(vm);
        }

    }
}
