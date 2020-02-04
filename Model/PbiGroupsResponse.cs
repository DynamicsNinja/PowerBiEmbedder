using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class PbiGroupsResponse
    {
        [JsonProperty("value")]
        public List<PbiGroup> Groups { get; set; }
    }
}
