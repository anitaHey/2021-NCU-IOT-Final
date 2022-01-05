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
using GalaSoft.MvvmLight.Messaging;

namespace IOTFinalServer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        DatabaseManager database;
        public RelayCommand ToUnfinished { get; private set; }
        public RelayCommand ToFinished { get; private set; }
        public RelayCommand OpenOrder { get; private set; }
        public RelayCommand OpenPosition { get; private set; }
        public ConnectServer server;

        public MainViewModel()
        {
            server = new ConnectServer();

            database = new DatabaseManager();
            UnfinishedServiceList = new ObservableCollection<ServiceData>();
            FinishedServiceList = new ObservableCollection<ServiceData>();

            ServiceList = database.loadService();
            ServiceState = 0;

            ToUnfinished = new RelayCommand(() => {
                ServiceState = 0;
            });

            ToFinished = new RelayCommand(() => {
                ServiceState = 1;
            });

            OpenOrder = new RelayCommand(() => {
                if (server.isZenboConnect())
               {
                    SendToZenboData place = new SendToZenboData();
                    place.type = "response_table";
                    place.table_id = 1;
                    
                    place.x = ((getPointXY("A").Y - getZenboXY().Y) / 100);
                    place.y = -((getPointXY("A").X - getZenboXY().X) / 100) ;

                     server.sendMsg(place);
                }
            });

            OpenPosition = new RelayCommand(() => { Messenger.Default.Send("open", "openPosition"); });

            Messenger.Default.Register<ObservableCollection<PointData>>(this, "PointItems", list => {
                PointItems = list;
            });
        }

        public PointData getZenboXY()
        {
            foreach(PointData tem in PointItems)
            {
                if(tem.pointName == "me")
                {
                    return tem;
                }
            }

            return null;
        }

        public PointData getPointXY(String name)
        {
            foreach (PointData tem in PointItems)
            {
                if (tem.pointName == name)
                {
                    return tem;
                }
            }

            return null;
        }

        private List<ServiceData> _serviceList;
        public List<ServiceData> ServiceList
        {
            get { return _serviceList; }
            set 
            {
                _serviceList = value;

                UnfinishedServiceList.Clear();
                FinishedServiceList.Clear();
                foreach (ServiceData tem in _serviceList)
                {
                    if (tem.state == 0)
                    {
                        UnfinishedServiceList.Add(tem);
                    } else if(tem.state == 1)
                    {
                        FinishedServiceList.Add(tem);
                    }
                }
            }
        }
 
        private ServiceData _serviceNode;
        public ServiceData ServiceNode
        {
            get { return _serviceNode; }
            set
            {
                _serviceNode = value;
            }
        }

        private int _serviceState;
        public int ServiceState
        {
            get { return _serviceState; }
            set
            {
                _serviceState = value;
                
                if(ServiceState == 0)
                {
                    ShowServiceList = UnfinishedServiceList;
                } else
                {
                    ShowServiceList = FinishedServiceList;
                }
            }
        }

        private ObservableCollection<ServiceData> _showServiceList;
        public ObservableCollection<ServiceData> ShowServiceList
        {
            get { return _showServiceList; }
            set
            {
                _showServiceList = value;
                RaisePropertyChanged(() => _showServiceList);
            }
        }

        private ObservableCollection<ServiceData> _unfinishedServiceList;
        public ObservableCollection<ServiceData> UnfinishedServiceList
        {
            get { return _unfinishedServiceList; }
            set
            {
                _unfinishedServiceList = value;
            }
        }

        private ObservableCollection<ServiceData> _finishedServiceList;
        public ObservableCollection<ServiceData> FinishedServiceList
        {
            get { return _finishedServiceList; }
            set
            {
                _finishedServiceList = value;
            }
        }

        public ObservableCollection<PointData> PointItems { get; set; }
    }
}
