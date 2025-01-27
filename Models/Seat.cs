using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Seat
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string SeatDegree { get; set; }
        public int SeatNo { get; set; }
        public Availability? Availability { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
