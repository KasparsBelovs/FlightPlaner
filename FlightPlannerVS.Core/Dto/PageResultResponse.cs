using System.Collections.Generic;

namespace FlightPlannerVS.Core.Dto
{
    public class PageResultResponse
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }

        public List<FlightResponse> Items = new List<FlightResponse>();
    }
}
