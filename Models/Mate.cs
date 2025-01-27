using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Mate
    {
        public int Id { get; set; }
        public int Infants { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}
