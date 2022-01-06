using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class DishData
    {
        public String name { get; set; }
        public int id { get; set; }
        public String content { get; set; }
        public int price { get; set; }
    }

    public class DishDataList
    {
        public ObservableCollection<DishData> list { get; set; } = new ObservableCollection<DishData>();
    }
    
}
