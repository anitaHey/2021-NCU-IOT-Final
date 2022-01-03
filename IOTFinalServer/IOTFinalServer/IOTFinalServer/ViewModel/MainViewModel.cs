using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Newtonsoft.Json;
using IOTFinalServer.Model;
using System.Collections.ObjectModel;

namespace IOTFinalServer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ServiceData> ServiceList { get; set; }

        private ServiceData _serviceNode;
        public ServiceData ServiceNode
        {
            get { return _serviceNode; }
            set
            {
                _serviceNode = value;
                RaisePropertyChanged(() => _serviceNode);
            }
        }
    }
}
