using FidsCodingAssignment.DataAccess;
using FidsCodingAssignment.Logic;
using System;
using System.IO;
using Xunit;

namespace FidsCodingAssignment.UnitTest
{
    public class FlightTest
    {
        private readonly string _testData;
        private readonly FlightSetting _flightSetting;
        public FlightTest()
        {
            string testData= $@"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\netcoreapp3.1\data\rawData.json";
            var data = File.ReadAllText(testData);

            _testData = data.Replace("testScheduleTime", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ssZ"));

            _testData = _testData.Replace("testActiveFlightAtGate", DateTime.Now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:ssZ"));

            _flightSetting = new FlightSetting()
            {
                BoardingWindowInMinutes = 45,
                FlightAtGateInMinutes = 15
            };
        }

        [Fact]
        public void GetFlightById()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.GetFlightById(123456);

            Assert.True(response != null);
        }

        [Fact]
        public void GetFlightByAirportCode()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria()
            { 
                AirportCode = "PH"
            });

            Assert.True(response.Count == 2);
        }

        [Fact]
        public void GetFlightByFlightNumber()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria
            {
                FlightNumber = 233
            });

            Assert.True(response.Count == 1);
        }

        [Fact]
        public void GetFlightByParentFlightNumber()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria
            {
                ParentFlightID = 123456
            });

            Assert.True(response.Count == 2);
        }

        [Fact]
        public void GetDelayedFlights()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria
            {
                IsFlightDelayed = true
            });

            Assert.True(response.Count != 0);
        }

        [Fact]
        public void GetDelayedWithMinutes()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria
            {
                IsFlightDelayed = true,
                MinutesDelayed = 200
            });

            Assert.True(response.Count != 0);
        }


        [Fact]
        public void GetIsAtGateActiveFlight()
        {
            var flightManager = new FlightManager(_testData, _flightSetting);

            var response = flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria
            {
                IsFlightAtGate = true
            });
        }
    }
}
