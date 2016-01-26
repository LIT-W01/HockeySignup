using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HockeySignup.Data;

namespace HockeySignup.Web.Models
{
    public class HistoryViewModel
    {
        public Event Event { get; set; }
        public IEnumerable<EventSignup> Signups { get;set; }
    }
}