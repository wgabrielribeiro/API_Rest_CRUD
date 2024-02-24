using RestWithAspNET.API.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspNET.API.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();

    }
}
