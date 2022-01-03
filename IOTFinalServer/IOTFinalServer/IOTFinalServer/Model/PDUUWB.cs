using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class PDUUWB
    {
        public enum Intent
        {
            Connect,
            TranferData,
        }

        public Intent intent { get; set; }
        public String msg { get; set; }
        public float base1 { get; set; }
        public float base2 { get; set; }
        public float base3 { get; set; }
        public float mainTag { get; set; }



    }
}
