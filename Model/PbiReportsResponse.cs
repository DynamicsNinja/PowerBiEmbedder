using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class PbiReportsResponse
    {
        [JsonProperty("value")]
        public List<PbiReport> Reports { get; set; }
    }
}
