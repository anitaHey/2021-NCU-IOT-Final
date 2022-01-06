using GalaSoft.MvvmLight.Messaging;
using IOTFinalServer.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOTFinalServer.Model
{
    public class ConnectServer
    {
        DatabaseManager database;
        IPEndPoint ipEnd;
        Socket serverSocket, ZenboConnect;
        Thread connectThread;
        public ConnectServer()
        {
            IPAddress ip = IPAddress.Parse(GetLocalIPv4());
            ipEnd = new IPEndPoint(ip, 6100);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            serverSocket.Bind(ipEnd);
            serverSocket.Listen(10);
            ZenboConnect = null;

            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();
            string ipv4 = GetLocalIPv4();
            Console.WriteLine(ipv4);

            database = new DatabaseManager();

            Messenger.Default.Register<ObservableCollection<PointData>>(this, "PointItems", list => {
                PointItems = list;
            });
        }

        public class SocketConnectData
        {
            public String name { get; set; }
            public Socket socket { get; set; }
        }

        public void SocketReceive()
        {
            while (true)
            {
                //一旦接受連線，建立一個客戶端
                Socket client = serverSocket.Accept();
                Thread th = new Thread(ReceiveMsg); // 开启客户端线程
                th.Start(client);
            }
        }

        public bool isZenboConnect()
        {
            return ZenboConnect != null;
        }

        public void ReceiveMsg(object client)
        {
            Socket connection = (Socket)client;
            IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
            Console.WriteLine("connect");
            while (SocketExtensions.IsConnected(connection))
            {
                try
                {
                    byte[] result = new byte[connection.Available];
                    int receive_num = connection.Receive(result);
                    String receive_str = Encoding.ASCII.GetString(result, 0, receive_num);
                    if (receive_num > 0)
                    {
                        GetCommandData message = JsonConvert.DeserializeObject<GetCommandData>(receive_str);
                        switch(message.type)
                        {
                            case "service_ring":
                                letZenboToPoint("response_service", message.table_id);
                                break;
                            case "order_ring":
                                letZenboToPoint("response_order", message.table_id);
                                break;
                            case "leave_ring":
                                letZenboToPoint("response_leave", message.table_id);
                                break;
                            case "find_table":
                                letZenboToPoint("response_table", message.table_id);
                                break;
                            case "order":
                                break;
                            case "zenbo_connect":
                                ZenboConnect = connection;

                                MenuData menu = new MenuData();
                                menu.type = "send_menu";
                                menu.list = database.loadMenu();

                                sendMenu(menu);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    //exception close()
                    Console.WriteLine(e);
                    if (connection.Connected)
                    {
                        connection.Shutdown(SocketShutdown.Both);
                        connection.Close();
                    }
                    break;
                }
            }
        }

        public void letZenboToPoint(String type, int table_id)
        {
            PointData table = getPointXY(table_id);
            SendToZenboData data = new SendToZenboData();
            data.table_id = table_id;
            data.type = type;
            data.x = (table.Y - getZenboXY().Y) / 100;
            data.y = -(table.X - getZenboXY().X) / 100;

            sendMsg(data);
        }

        public void letZenboToPoint(String type, int table_id, int order_id)
        {
            PointData table = getPointXY(table_id);
            SendToZenboData data = new SendToZenboData();
            data.table_id = table_id;
            data.type = type;
            data.x = (table.Y - getZenboXY().Y) / 100;
            data.y = -(table.X - getZenboXY().X) / 100;
            data.order_id = order_id;

            sendMsg(data);
        }

        public void sendMenu(MenuData json)
        {
            if (ZenboConnect != null)
            {
                string allJson = JsonConvert.SerializeObject(json);
                byte[] send = Encoding.Default.GetBytes(allJson + "\n");

                ZenboConnect.Send(send);

            }
        }

        public void sendMsg(SendToZenboData json)
        {
            if (ZenboConnect != null)
            {
                string allJson = JsonConvert.SerializeObject(json);
                byte[] send = Encoding.Default.GetBytes(allJson + "\n");

                ZenboConnect.Send(send);

            }
        }

        public string GetLocalIPv4()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList.First(
                    f => f.AddressFamily == AddressFamily.InterNetwork)
                .ToString();
        }

        public ObservableCollection<PointData> PointItems { get; set; }

        public PointData getZenboXY()
        {
            foreach (PointData tem in PointItems)
            {
                if (tem.pointName == "me")
                {
                    return tem;
                }
            }

            return null;
        }

        public PointData getPointXY(int id)
        {
            foreach (PointData tem in PointItems)
            {
                if (tem.id == id)
                {
                    return tem;
                }
            }

            return null;
        }
    }
}
