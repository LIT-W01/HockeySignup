using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeySignup.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new HockeySignupDb(Settings1.Default.ConStr);
            Event e = new Event {Date = DateTime.Today.AddDays(3), MaxPeople = 20};
            db.AddEvent(e);
            Console.WriteLine(e.Id);
            Console.ReadKey(true);
        }
    }
}
