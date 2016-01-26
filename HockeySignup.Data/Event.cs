using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeySignup.Data
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MaxPeople { get; set; }
    }

    public class EventWithCount : Event
    {
        public int PeopleCount { get; set; }
    }
}
