using System.Collections.Generic;

namespace RestWithAspNET.API.Hypermedia.Abstract
{
    public interface ISuporteHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
