using FidsCodingAssignment.Common.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FidsCodingAssignment.Common.Model
{
    public class FlightChild
    {
        public long? FlightNumber { get; set; }
        public string AirlineCode { get; set; }
    }
}
