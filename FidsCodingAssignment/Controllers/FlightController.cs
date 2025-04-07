using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FidsCodingAssignment.Logic;
using AutoMapper;
using FidsCodingAssignment.DataAccess;

namespace FidsCodingAssignment.Controllers
{
    [ApiController]
    [Route("flights")]
    public class FlightController : ControllerBase
    {
        private string _data;
        private FlightSetting _setting;
        private readonly IMapper _mapper;
        public FlightController(string data,
            FlightSetting setting,
            IMapper mapper)
        {
            _data = data;
            _setting = setting;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all flights
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Client.Model.Flight>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlightListAsync()
        {
            var flightManager = new FlightManager(_data, _setting);

            var response = await Task.Run(() => flightManager.GetFlightList());

            var apiModels = response.Select(
                m => _mapper.Map<Client.Model.Flight>(m)).ToList();

            return Ok(apiModels);
        }

        /// <summary>
        /// Search flight by search criteria
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("search")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Client.Model.Flight>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDelayedFlightsAsync([FromQuery] Client.Model.Search.FlightSearchCriteria criteria)
        {
            var flightManager = new FlightManager(_data, _setting);

            var logic = _mapper.Map<Common.Model.Search.FlightSearchCriteria>(criteria);

            var response = await Task.Run(() => flightManager.SearchFlights(logic));

            var apiModels = response.Select(
                m => _mapper.Map<Client.Model.Flight>(m)).ToList();

            return Ok(apiModels);
        }

        /// <summary>
        /// Get flight by id
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Client.Model.Flight), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlightByIdAsync(long id)
        {
            var flightManager = new FlightManager(_data, _setting);

            var response = await Task.Run(() => flightManager.GetFlightById(id));

            var apiModels = _mapper.Map<Client.Model.Flight>(response);

            return Ok(apiModels);
        }

        /// <summary>
        /// Get flight status by id
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("{id}/status")]
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlightStatusByIdAsync(long id)
        {
            var flightManager = new FlightManager(_data, _setting);

            var response = await Task.Run(() => flightManager.GetFlightById(id));

            var apiModels = _mapper.Map<Client.Model.Flight>(response);

            return Ok(apiModels.Status);
        }

        /// <summary>
        /// Get flights that are at the gate that are active
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("gate/active")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Client.Model.Flight>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchActiveAtTheGateFlightsAsync()
        {
            var flightManager = new FlightManager(_data, _setting);

            var response = await Task.Run(() => flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria()
            {
                IsFlightAtGate = true
            }));

            var apiModels = response.Select(
                m => _mapper.Map<Client.Model.Flight>(m)).ToList();

            return Ok(apiModels);
        }

        /// <summary>
        /// Get delayed flights that are less than the minutes set, if the minutes set is empty, return all delayed flights
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">response could not be parsed</response>
        [Route("delayed")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Client.Model.Flight>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDelayedFlightsAsync(double? minute)
        {
            var flightManager = new FlightManager(_data, _setting);

            var response = await Task.Run(() => flightManager.SearchFlights(new Common.Model.Search.FlightSearchCriteria()
            {
                IsFlightDelayed = true,
                MinutesDelayed = minute
            }));

            var apiModels = response.Select(
                m => _mapper.Map<Client.Model.Flight>(m)).ToList();

            return Ok(apiModels);
        }
    }
}