using AutoMapper;
using FidsCodingAssignment.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FidsCodingAssignment.Client
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.Model.Flight, Client.Model.Flight>();
            CreateMap<Common.Model.FlightChild, Client.Model.FlightChild>();
            CreateMap<Client.Model.Search.FlightSearchCriteria, Common.Model.Search.FlightSearchCriteria>();
        }
    }
}
