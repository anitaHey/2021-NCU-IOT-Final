using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class GetCommandData
    {
        public String type { get; set; }
        public int table_id { get; set; }
        public int customer_number { get; set; }
        public int dish_id { get; set; }
        public int order_id { get; set; }
    }
}
