﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class OrderData
    {
        public int order_id { get; set; }
        public int table_id { get; set; }
        public int state { get; set; }
        public String orderTime { get; set; }
        public String checkTime { get; set; }
    }
}
