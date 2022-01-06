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
                Messenger.Default.Send("open", "openOrder");
            });

            OpenPosition = new RelayCommand(() => { Messenger.Default.Send("open", "openPosition"); });
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
                RaisePropertyChanged(() => ShowServiceList);
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
    }
}
