using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public double Miles { get; set; }
        public int Age { get; set; }
        public int Point { get; set; }
        public ICollection<Mate> Mates { get; set; } = new List<Mate>();
        public ICollection<TicketPassenger> TicketPassengers { get; set; } = new List<TicketPassenger>();
    }
}
