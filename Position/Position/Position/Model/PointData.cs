using System;

namespace Position.Model
{
    public class PointData
    {
        public PointData(String name, String color)
        {
            this.pointName = name;
            this.color = color;
            this.x = 0;
            this.y = 0;
        }

        public String pointName { get; set; }
        public float x { get; set; }
        public float y { get; set; }

        public String color { get; set; }
    }
}
