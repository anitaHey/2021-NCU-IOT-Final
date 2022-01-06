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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace IOTFinalServer.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        DatabaseManager database;
        public RelayCommand ToUnfinished { get; private set; }
        public RelayCommand ToFinished { get; private set; }
        public RelayCommand OpenService { get; private set; }
        public RelayCommand OpenPosition { get; private set; }
        public ConnectServer server;

        public OrderViewModel()
        {
            server = new ConnectServer();

            database = new DatabaseManager();
            UnfinishedOrderList = new ObservableCollection<OrderData>();
            FinishedOrderList = new ObservableCollection<OrderData>();

            OrderList = database.loadOrder();
            OrderState = 0;

            ToUnfinished = new RelayCommand(() => {
                OrderState = 0;
            });

            ToFinished = new RelayCommand(() => {
                OrderState = 1;
            });

            OpenService = new RelayCommand(() => {
                Messenger.Default.Send("open", "openService");
            });

            OpenPosition = new RelayCommand(() => { Messenger.Default.Send("open", "openPosition"); });
        }

        private List<OrderData> _orderList;
        public List<OrderData> OrderList
        {
            get { return _orderList; }
            set
            {
                _orderList = value;

                UnfinishedOrderList.Clear();
                FinishedOrderList.Clear();
                foreach (OrderData tem in _orderList)
                {
                    if (tem.state == 0)
                    {
                        UnfinishedOrderList.Add(tem);
                    }
                    else if (tem.state == 1)
                    {
                        FinishedOrderList.Add(tem);
                    }
                }
            }
        }

        private OrderData _orderNode;
        public OrderData OrderNode
        {
            get { return _orderNode; }
            set
            {
                _orderNode = value;
            }
        }

        private int _orderState;
        public int OrderState
        {
            get { return _orderState; }
            set
            {
                _orderState = value;

                if (OrderState == 0)
                {
                    ShowOrderList = UnfinishedOrderList;
                }
                else
                {
                    ShowOrderList = FinishedOrderList;
                }
            }
        }

        private ObservableCollection<OrderData> _showOrderList;
        public ObservableCollection<OrderData> ShowOrderList
        {
            get { return _showOrderList; }
            set
            {
                _showOrderList = value;
                RaisePropertyChanged(() => ShowOrderList);
            }
        }

        private ObservableCollection<OrderData> _unfinishedOrderList;
        public ObservableCollection<OrderData> UnfinishedOrderList
        {
            get { return _unfinishedOrderList; }
            set
            {
                _unfinishedOrderList = value;
            }
        }

        private ObservableCollection<OrderData> _finishedOrderList;
        public ObservableCollection<OrderData> FinishedOrderList
        {
            get { return _finishedOrderList; }
            set
            {
                _finishedOrderList = value;
            }
        }
    }
}
