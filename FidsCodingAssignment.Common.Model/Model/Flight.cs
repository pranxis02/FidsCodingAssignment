using FidsCodingAssignment.Common.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FidsCodingAssignment.Common.Model
{
    public partial class Flight
    {
        public bool? IsBoardingTime { get; set; }
        public bool? IsFlightAtGate { get; set; }
        public bool? IsFlightDelayed { get; set; } = false;
        public double DelayedInMinutes { get; set; } = 0;
        public BoardingStatusEnum? Status { get; set; }
        public List<FlightChild> ChildrenFlights { get; set; } = new List<FlightChild>();
    }

    public partial class Flight
    {
        [JsonProperty("flightid")]
        public long? Id { get; set; }

        [JsonProperty("parentflightid")]
        public long? ParentFlightID { get; set; }

        [JsonProperty("parentfltnumber")]
        public string ParentFlightNumber { get; set; }

        [JsonProperty("flightnumber")]
        public long? FlightNumber { get; set; }

        [JsonProperty("flighttype")]
        public string FlightType { get; set; }

        [JsonProperty("flightstatuscode")]
        public string FlightStatus { get; set; }

        [JsonProperty("dep_boardingstart_dtm")]
        public DateTime? BoardingTime { get; set; }

        [JsonProperty("sched_time")]
        public DateTime? ScheduleTime { get; set; }

        [JsonProperty("actual_time")]
        public DateTime? ActualTime { get; set; }

        [JsonProperty("estimated_time")]
        public DateTime? EstimatedTime { get; set; }

        [JsonProperty("remote_airport_sch_dtm")]
        public DateTime? RemoteAirportScheduleDateTime { get; set; }

        [JsonProperty("remote_airport_act_dtm")]
        public DateTime? RemoteAirportActualDateTime { get; set; }

        [JsonProperty("remote_airport_est_dtm")]
        public DateTime? RemoteAirportEstimatedDateTime { get; set; }

        [JsonProperty("airline_name")]
        public string AirlineName { get; set; }

        [JsonProperty("parentairlinecode")]
        public string ParentAirlineCode { get; set; }

        [JsonProperty("airlinecode")]
        public string AirlineCode { get; set; }

        [JsonProperty("viaairportcodes")]
        public List<string> ViaAirportCodes { get; set; }

        [JsonProperty("airportcode")]
        public string AirportCode { get; set; }

        [JsonProperty("terminalcode")]
        public string TerminalCode { get; set; }

        [JsonProperty("gatecode")]
        public string GateID { get; set; }

        [JsonProperty("city_name")]
        public string Location { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("aircraftregnumber")]
        public string AircraftRegisteredNumber { get; set; }

        [JsonProperty("aircrafttype")]
        public string AircraftType { get; set; }

        [JsonProperty("bagbelt")]
        public string BagBelt { get; set; }

        [JsonProperty("tail")]
        public string Tail { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("eventtime")]
        public DateTime? EventTime { get; set; }

        [JsonProperty("parrentsuffix")]
        public string ParentSuffix { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("arrdep")]
        public string Type { get; set; }
    }

}
