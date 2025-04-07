using System;
using System.Collections.Generic;
using System.Text;

namespace FidsCodingAssignment.Common.Model.Search
{
    public class FlightSearchCriteria
    {
        public long? Id { get; set; }
        public long? FlightNumber { get; set; }
        public string AirportCode { get; set; }
        public long? ParentFlightID { get; set; }
        public bool? IsFlightDelayed { get; set; }
        public double? MinutesDelayed { get; set; }
        public bool? IsFlightAtGate { get; set; }
    }
}
