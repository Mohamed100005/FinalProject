using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey("PassengerId", "TicketId")]
    public class TicketPassenger
    {
        public int PassengerId { get; set; }
        [ForeignKey("PassengerId")]
        public Passenger Passenger { get; set; }
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }
}
