using FidsCodingAssignment.Client.Enum;
using System;
using System.Collections.Generic;

namespace FidsCodingAssignment.Client.Model
{
    public partial class Flight
    {
        public string Type { get; set; }
        public long? FlightNumber { get; set; }
        public string AirlineCode { get; set; }
        public BoardingStatusEnum? Status { get; set; }
        public string GateID { get; set; }
        public bool? IsFlightAtGate { get; set; }
        public bool? IsFlightDelayed { get; set; }
        public double DelayedInMinutes { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public DateTime? ActualTime { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public List<FlightChild> ChildrenFlights { get; set; } = new List<FlightChild>();
    }
}
