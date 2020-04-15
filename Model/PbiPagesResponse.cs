using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class PbiPagesResponse
    {
        [JsonProperty("value")]
        public List<PbiPage> Pages { get; set; }
    }
}
