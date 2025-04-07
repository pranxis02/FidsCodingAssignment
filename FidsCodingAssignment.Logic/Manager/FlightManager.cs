using FidsCodingAssignment.Common.Enum;
using FidsCodingAssignment.Common.Model;
using FidsCodingAssignment.Common.Model.Search;
using FidsCodingAssignment.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FidsCodingAssignment.Logic
{
    public class FlightManager
    {
        private string _data;
        private FlightSetting _setting;

        public FlightManager(string data, FlightSetting setting)
        {
            _data = data;
            _setting = setting;
        }

        public List<Flight> GetFlightList()
        {
            var flightDao = new FlightDAO(_data, _setting);

            var flights = flightDao.GetFlightList();

            return flights;
        }

        public Flight GetFlightById(long id)
        {
            var flightDao = new FlightDAO(_data, _setting);

            var flight = flightDao.GetFlightById(id);

            return flight;
        }

        public List<Flight> SearchFlights(FlightSearchCriteria criteria)
        {
            var flightDao = new FlightDAO(_data, _setting);

            var flights = flightDao.GetFlightList();

            var resultFlights = flights.Where(x => x.Id == (criteria.Id ?? x.Id)
                                               && x.FlightNumber == (criteria.FlightNumber ?? x.FlightNumber)
                                               && x.AirportCode == (String.IsNullOrWhiteSpace(criteria.AirportCode) ? x.AirportCode : criteria.AirportCode)
                                               && x.ParentFlightID == (criteria.ParentFlightID ?? x.ParentFlightID)
                                               && x.IsFlightDelayed == (criteria.IsFlightDelayed ?? x.IsFlightDelayed)
                                               && x.DelayedInMinutes <= (criteria.MinutesDelayed ?? x.DelayedInMinutes)
                                               && x.IsFlightAtGate == (criteria.IsFlightAtGate ?? x.IsFlightAtGate)).ToList();

            return resultFlights;
        }
    }
}
