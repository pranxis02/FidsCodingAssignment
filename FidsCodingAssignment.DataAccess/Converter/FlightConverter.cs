using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using FidsCodingAssignment.Common.Model;

namespace FidsCodingAssignment.DataAccess.Converter
{
    public class FlightConverter : JsonConverter<Flights>
    {
        public override Flights ReadJson(JsonReader reader, Type objectType, Flights existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject json = JObject.Load(reader);

            var properties = json.Properties().ToList();

            var flights = new Flights
            {
                FlightListName = properties[0].Name.ToString(),
                FlightList = JsonConvert.DeserializeObject<List<Flight>>(properties[0].Value.ToString()),
                Status = properties[1].Value.ToString()
            };

            return flights;
        }

        public override void WriteJson(JsonWriter writer, Flights value, JsonSerializer serializer)
        {

        }
    }
}
