using RestWithAspNET.API.Hypermedia;
using RestWithAspNET.API.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithAspNET.API.Data.VO
{
    public class PersonVO : ISuporteHypermedia
    {
        //[JsonPropertyName("code")]
        public long Id { get; set; }
        //[JsonPropertyName("name")]
        public string Firstname { get; set; }
        //[JsonPropertyName("last_name")]
        public string LastName { get; set; }
        //[JsonIgnore]
        public string Address { get; set; }
        //[JsonIgnore]
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
