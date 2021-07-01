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
                cfg.CreateMap<FlightRequest, Flight>().ForMember(x =>
                    x.Id, o => o.Ignore());
                cfg.CreateMap<Flight, FlightRequest>();
                
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}