using AutoMapper;
using FlightPlannerVS.Core.Dto;
using FlightPlannerVS.Core.Models;

namespace FlightPlannerVS.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<FlightRequest, Flight>()
                    .ForMember(d => d.Id,
                        o => o.Ignore());
                cfg.CreateMap<Flight, FlightRequest>();

                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.AirportName,
                        o => o
                            .MapFrom(s => s.Airport))
                    .ForMember(d => d.Id,
                        o => o.Ignore());
                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport,
                        o => o
                            .MapFrom(s => s.AirportName));

                cfg.CreateMap<Flight, FlightResponse>();
                cfg.CreateMap<FlightResponse, Flight>();

                cfg.CreateMap<AirportResponse, Airport>()
                    .ForMember(d => d.AirportName,
                        o => o
                            .MapFrom(s => s.Airport))
                    .ForMember(d => d.Id,
                        s => s.Ignore());
                cfg.CreateMap<Airport, AirportResponse>()
                    .ForMember(d => d.Airport,
                        o => o
                            .MapFrom(s => s.AirportName));

            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}