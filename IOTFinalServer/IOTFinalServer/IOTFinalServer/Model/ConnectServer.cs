using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        IPEndPoint ipEnd;
        Socket serverSocket, ZenboConnect;
        Thread connectThread;
        public ConnectServer()
        {
            IPAddress ip = IPAddress.Parse(GetLocalIPv4());
            ipEnd = new IPEndPoint(ip, 7001);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEnd);
            serverSocket.Listen(10);
            ZenboConnect = null;

            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();
            string ipv4 = GetLocalIPv4();
            Console.WriteLine(ipv4);
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
            ZenboConnect = connection;
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
                        Console.WriteLine(receive_str);
                        //    if (clientdata == null)
                        //    {
                        //        clientdata = JsonConvert.DeserializeObject<ClientData>(receive_str);
                        //        clientdata.socket = connection;

                        //        System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                        //        {
                        //            curStatus.onlineClient.List.Add(clientdata);
                        //        });

                        //        string text = clientdata.FullName + " is entered the chatroom!";
                        //        MessageData message = new MessageData() { client = system, Message = text };

                        //        System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                        //        {
                        //            curStatus.messageList.List.Add(message);
                        //        });
                        //    }
                        //    else
                        //    {
                        //        MessageData message = JsonConvert.DeserializeObject<MessageData>(receive_str);
                        //        string send_str = message.client.FullName + ":" + message.Message;

                        //        System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        //        {
                        //            curStatus.messageList.List.Add(message);
                        //        });
                        //    }

                        //    updateRoom(curStatus);
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
    }
}
