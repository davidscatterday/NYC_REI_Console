using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYC_REI_Console.Entities
{
    class JsonBlsData
    {
        public string status { get; set; }
        public string responseTime { get; set; }
        public List<string> message { get; set; }
        public BlsResults Results { get; set; }
    }
    class BlsResults
    {
        public List<BlsSeries> series { get; set; }
    }
    class BlsSeries
    {
        public string seriesID { get; set; }
        public List<BlsData> data { get; set; }
    }
    class BlsData
    {
        public string year { get; set; }
        public string period { get; set; }
        public string periodName { get; set; }
        public string value { get; set; }
    }
}
