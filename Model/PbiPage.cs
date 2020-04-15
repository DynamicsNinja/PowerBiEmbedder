using Newtonsoft.Json;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class PbiPage
    {
        public string Name { get; set; }
        [JsonProperty("displayName")]

        public string DisplayName { get; set; }
        public int Order { get; set; }
    }
}
