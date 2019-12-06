using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Fic.XTB.PowerBiEmbedder.Model
{

    public class PbiFilter
    {
        [JsonProperty("Filter")]
        public string FilterString { get; set; }
        [JsonIgnore]
        public Filter Filter { get; set; }
        public Alias Alias { get; set; }

        [JsonConstructor]
        public PbiFilter() {
            
        }
        public PbiFilter(string json) {
            var fixedJson = json.Replace("[$a]", "[\\\"$a\\\"]");

            var deserialized = JsonConvert.DeserializeObject<PbiFilter>(fixedJson);
            this.FilterString = deserialized.FilterString;
            this.Alias = deserialized.Alias;
            this.Filter = (JsonConvert.DeserializeObject<Filter[]>(deserialized.FilterString)).FirstOrDefault();
        }

        public PbiFilter(string pbiTable, string pbiColumn, string cdsField) {
            var filter = new[] {
                new Filter {
                    Schema = "basic",
                    Target = new Target {
                        Table = pbiTable,
                        Column = pbiColumn
                    },
                    Operator = "In",
                    Values = new[] { "$a" },
                    FilterType = 1
                }
            };

            var escapedJsonFilter = JsonConvert.SerializeObject(filter);

            Filter = filter.FirstOrDefault();
            FilterString = escapedJsonFilter;
            Alias = new Alias { A = cdsField };
        }

        public string ToJsonString() {
            var jsonString = JsonConvert.SerializeObject(this);
            return jsonString.Replace("[\\\"$a\\\"]", "[$a]");
        }
    }

    public class Filter {
        [JsonProperty("$schema")]
        public string Schema { get; set; }
        [JsonProperty("target")]
        public Target Target { get; set; }
        [JsonProperty("operator")]
        public string Operator { get; set; }
        [JsonProperty("values")]
        public string[] Values { get; set; }
        [JsonProperty("filterType")]
        public int FilterType { get; set; }
    }
    public class Alias
    {
        [JsonProperty("$a")]
        public string A { get; set; }
    }

    public class Target
    {
        [JsonProperty("table")]
        public string Table { get; set; }
        [JsonProperty("column")]
        public string Column { get; set; }
    }
}
