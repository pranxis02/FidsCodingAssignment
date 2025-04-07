using FidsCodingAssignment.Common.Enum;
using FidsCodingAssignment.Common.Model;
using FidsCodingAssignment.DataAccess.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FidsCodingAssignment.DataAccess
{
    public class FlightSetting
    {
        public int BoardingWindowInMinutes { get; set; }
        public int FlightAtGateInMinutes { get; set; }
    }

    public class FlightDAO
    {
        private List<Flight> _flights;
        private FlightSetting _flightSetting;

        public FlightDAO(string data,
                        FlightSetting flightSetting) 
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new FlightConverter() }
            };

            var response = JsonConvert.DeserializeObject<Flights>(data, settings);

            _flightSetting = flightSetting;
            _flights = response.FlightList;

            SetData(_flights);
        }

        public List<Flight> GetFlightList()
        {
            return _flights;
        }

        public Flight GetFlightById(long id)
        {
            var flight = _flights.Where(x => x.Id == id).FirstOrDefault();

            return flight;
        }

        private void SetData(List<Flight> flights)
        {
            flights.ForEach(flight =>
            {
                if (String.Equals(flight.Type, "ARR"))
                    flight.Type = FlightTypeEnum.Arrival.ToString();

                if (String.Equals(flight.Type, "DEP"))
                    flight.Type = FlightTypeEnum.Departure.ToString();

                flight.ChildrenFlights.AddRange(
                    flights.Where(childrenFlight => childrenFlight.ParentFlightID == flight.Id)
                    .Select(child => new FlightChild()
                    {
                        AirlineCode = child.AirlineCode,
                        FlightNumber = child.FlightNumber
                    }).ToList());

                if (flight.ScheduleTime.HasValue)
                {
                    TimeSpan schedule = flight.ScheduleTime.Value - DateTime.Now;

                    if (schedule.TotalMinutes < 0)
                    {
                        flight.IsFlightDelayed = true;
                        flight.DelayedInMinutes = Math.Round(-(schedule.TotalMinutes), 2);
                    }

                    flight.IsBoardingTime = (schedule.TotalMinutes >= 0 && schedule.TotalMinutes <= _flightSetting.BoardingWindowInMinutes && flight.Type == FlightTypeEnum.Departure.ToString());

                    flight.Status = (flight.IsBoardingTime.Value) ? BoardingStatusEnum.Boarding : BoardingStatusEnum.Closed;

                    TimeSpan flightAtGateDeparture = (flight.ActualTime ?? flight.ScheduleTime.Value) - DateTime.Now;
                    TimeSpan flightAtGateArrival = DateTime.Now - (flight.ActualTime ?? flight.ScheduleTime.Value);

                    var isFlightAtGate = (flight.Type == FlightTypeEnum.Departure.ToString() && flightAtGateDeparture.TotalMinutes >= 0 && flightAtGateDeparture.TotalMinutes <= _flightSetting.FlightAtGateInMinutes)
                        || (flight.Type == FlightTypeEnum.Arrival.ToString() && flightAtGateArrival.TotalMinutes >= 0 && flightAtGateArrival.TotalMinutes <= _flightSetting.FlightAtGateInMinutes);

                    flight.IsFlightAtGate = isFlightAtGate;
                }
            });
        }
    }
}
