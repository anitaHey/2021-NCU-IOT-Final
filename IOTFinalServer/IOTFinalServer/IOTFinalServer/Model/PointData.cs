using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class PointData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(
                [System.Runtime.CompilerServices.CallerMemberName]
            string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PointData()
        {
            this.color = "#ceefe4";
            this.number = 2;
            this.state = 0;
        }

        public PointData(String name, String color)
        {
            this.id = -1;
            this.pointName = name;
            this.color = color;
            this.X = 0;
            this.Y = 0;
            this.number = 2;
            this.state = 0;
        }

        public int id { get; set; }
        public String pointName { get; set; }
        private double _x { get; set; }
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }
        private double _y { get; set; }
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }

        public String color { get; set; }

        public int number { get; set; }
        public int state { get; set; }
    }
}
