using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Price { get; set; }
        public DateOnly ReturnDate { get; set; }
        public DateOnly DepartDate { get; set; }
        public ICollection<TicketTrip> TicketTrips { get; set; } = new List<TicketTrip>();
        public ICollection<Seat> seats { get; set; } = new List<Seat>();
    }
}
