using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class SendToZenboData
    {
        public String type { get; set; }
        public int table_id { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}
