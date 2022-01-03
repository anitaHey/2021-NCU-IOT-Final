using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class PointData
    {
        public PointData()
        {
            this.color = "#ceefe4";
            this.number = 2;
            this.state = 0;
        }

        public PointData(String name, String color)
        {
            this.pointName = name;
            this.color = color;
            this.x = 0;
            this.y = 0;
            this.number = 2;
            this.state = 0;
        }

        public String pointName { get; set; }
        public double x { get; set; }
        public double y { get; set; }

        public String color { get; set; }

        public int number { get; set; }
        public int state { get; set; }
    }
}
