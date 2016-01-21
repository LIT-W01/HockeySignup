using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HockeySignup.Data;

namespace HockeySignup.Web.Models
{
    public class SignupViewModel
    {
        public Event Event { get; set; }
        public EventStatus Status { get; set; }
    }
}