using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidsCodingAssignment.Common.Model
{
    public class Flights
    {
        public string FlightListName { get; set; }
        public List<Flight> FlightList { get; set; }
        public string Status { get; set; }
    }
}
