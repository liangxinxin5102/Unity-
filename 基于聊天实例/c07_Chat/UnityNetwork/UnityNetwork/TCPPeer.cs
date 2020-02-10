using System;
using System.Net;
using System.Net.Sockets;

namespace UnityNetwork
{
    public class TCPPeer
    {
        // 用来判断TCPPeer实例是客户端还是服务器
        // 如果是客户端，Socket则用于连接服务器，否则用于监听来自客户端的连接。
        public bool isServer { set;  get; }
        // 使用的socket
        public Socket socket;
        // 网络管理器
        NetworkManager networkMgr;

        // 在构造函数中需要传入NetworkManager对象
        public TCPPeer ( NetworkManager netMgr )
        {
            networkMgr = netMgr;
        }
       
        // 作为服务器，开始监听
        public void Listen( string ip, int port, int backlog=1000 )
        {
            isServer = true;
            // ip地址
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            // 创建socket
            socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // 将socket绑定到地址上
                socket.Bind(ipe);
                // 开始监听
                socket.Listen(backlog);
                // 开始异步接受连接
                socket.BeginAccept(new System.AsyncCallback(ListenCallback), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 异步接受一个新的连接, 如果连接成功，取得远程客户端的Socket，开始异步接收客户端发来的数据
        void ListenCallback(System.IAsyncResult ar)
        {
            // 取得服务器socket
            Socket listener = (Socket)ar.AsyncState;
            try
            {
                // 获得客户端的socket
                Socket client = listener.EndAccept(ar);

                // 通知服务器接受一个新的连接
                WriteMsg("OnAccepted", client);

                // 创建接收数据的数据包
                NetPacket packet = new NetPacket();
                packet.socket = client;
                // 开始接收从来自客户端的数据
                client.BeginReceive(packet.bytes, 0, NetPacket.headerLength, SocketFlags.None, new System.AsyncCallback(ReceiveHeader), packet);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // 继续接受其它连接
            listener.BeginAccept(new System.AsyncCallback(ListenCallback), listener);
        }

        // 作为客户端，开始异步连接服务器
        public void Connect( string ip, int port )
        {
            isServer = false;
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 开始连接
                socket.BeginConnect(ipe, new System.AsyncCallback(ConnectionCallback), socket);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 客户端异步连接回调
        void ConnectionCallback(System.IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                // 与服务器取得连接
                client.EndConnect(ar);

                // 通知已经成功连接到服务器
                WriteMsg("OnConnected", client);

                // 开始异步接收服务器信息
                NetPacket packet = new NetPacket();
                packet.socket = client;
                client.BeginReceive(packet.bytes, 0, NetPacket.headerLength, SocketFlags.None, new System.AsyncCallback(ReceiveHeader), packet);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                WriteMsg("OnConnectFailed", client);
            }
        }

        // 无论是创建用于监听的服务器Socket还是用于发起连接的客户端Socket，
        // 最后都会进入接收数据状态。
        // 接收数据主要是通过ReceiveHeader和ReceiveBody两个函数。
        void ReceiveHeader(System.IAsyncResult ar)
        {
            NetPacket packet = (NetPacket)ar.AsyncState;
            try
            {
                // 返回网络上接收的数据长度
                int read = packet.socket.EndReceive(ar);
                // 已断开连接
                if (read < 1)
                {
                    // 通知丢失连接
                    WriteMsg("OnLost", packet.socket);
                    return;
                }

                packet.readLength += read;
                // 消息头必须读满4个字节
                if (packet.readLength < NetPacket.headerLength)
                {
                    packet.socket.BeginReceive(packet.bytes,
                        packet.readLength,                          // 存储偏移已读入的长度
                        NetPacket.headerLength - packet.readLength, // 这次只读入剩余的数据
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveHeader),
                        packet);
                }
                else
                {
                    // 获得消息体长度
                    packet.DecodeHeader();

                    packet.readLength = 0;
                    // 开始读取消息体
                    packet.socket.BeginReceive(packet.bytes,
                        NetPacket.headerLength,
                        packet.bodyLenght,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveBody),
                        packet);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ReceiveHeader:"+ex.Message);
            }
        }

        // 接收体消息
        void ReceiveBody(System.IAsyncResult ar)
        {
            NetPacket packet = (NetPacket)ar.AsyncState;

            try
            {
                // 返回网络上接收的数据长度
                int read = packet.socket.EndReceive(ar);
                // 已断开连接
                if (read < 1)
                {
                    // 通知丢失连接
                    WriteMsg("OnLost", packet.socket);
                    return;
                }
                packet.readLength += read;

                // 消息体必须读满指定的长度
                if ( packet.readLength < packet.bodyLenght )
                {
                    packet.socket.BeginReceive(packet.bytes,
                        NetPacket.headerLength + packet.readLength,
                        packet.bodyLenght - packet.readLength,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveBody),
                        packet);
                }
                else
                {
                    // 将消息传入到逻辑处理队列
                    NetPacket newpacket = new NetPacket(packet);
                    networkMgr.AddPacket(newpacket);

                    // 下一个读取
                    packet.Reset();
                    packet.socket.BeginReceive(packet.bytes,
                        0,
                        NetPacket.headerLength,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveHeader),
                        packet);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ReceiveBody:" + ex.Message);
            }
        }

        // 向远程发送消息
        public void Send( Socket sk, NetPacket packet  )
        {
            NetworkStream ns;
            lock (sk)
            {
                ns = new NetworkStream(sk);
                if (ns.CanWrite)
                {
                    try
                    {
                        ns.BeginWrite( packet.bytes, 0, packet.Length, new System.AsyncCallback(SendCallback), ns);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        //发送回调
        private void SendCallback(System.IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            try
            {
                ns.EndWrite(ar);
                ns.Flush();
                ns.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 向NetworkManager对象发送消息，如连接失败，连接成功等。
        private void WriteMsg(string msg, Socket sk )
        {
            // 通知丢失连接
            NetPacket p = new NetPacket();
            p.socket = sk;
            p.BeginWrite(msg);
            networkMgr.AddPacket(p);
        }
    }
}
