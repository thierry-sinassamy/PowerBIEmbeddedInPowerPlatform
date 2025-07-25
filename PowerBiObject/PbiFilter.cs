using Newtonsoft.Json;

namespace PowerBiEmbedder.PowerBiObject
{
    public class PbiFilter
    {
        [JsonProperty("Filter")]
        public string FilterString { get; set; }

        [JsonIgnore]
        public Filter Filter { get; set; }

        public Alias Alias { get; set; }

        [JsonConstructor]
        public PbiFilter()
        {
        }

        public PbiFilter(string json)
        {
            PbiFilter pbiFilter = JsonConvert.DeserializeObject<PbiFilter>(json.Replace("[$a]", "[\\\"$a\\\"]"));
            this.FilterString = pbiFilter.FilterString;
            this.Alias = pbiFilter.Alias;
            this.Filter = ((IEnumerable<Filter>)JsonConvert.DeserializeObject<Filter[]>(pbiFilter.FilterString)).FirstOrDefault<Filter>();
        }
        public PbiFilter(string pbiTable, string pbiColumn, string cdsField)
        {
            Filter[] source = new Filter[1]
            {
                new Filter()
                {
          Schema = "basic",
          Target = new Target()
          {
            Table = pbiTable,
            Column = pbiColumn
          },
          Operator = "In",
          Values = new string[1]{ "$a" },
          FilterType = 1
        }
            };
            string str = JsonConvert.SerializeObject((object)source);
            this.Filter = ((IEnumerable<Filter>)source).FirstOrDefault<Filter>();
            this.FilterString = str;
            this.Alias = new Alias() { A = cdsField };
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject((object)this).Replace("[\\\"$a\\\"]", "[$a]");
        }
    }
}
