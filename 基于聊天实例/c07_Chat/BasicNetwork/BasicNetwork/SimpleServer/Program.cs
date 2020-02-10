using System;
using System.Net;
using System.Net.Sockets;

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 服务器ip地址
            string ip = "127.0.0.1";
            // 服务器端口
            int port = 8000;

            try
            {
                // 获得终端
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
                // 创建Socket
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 将Socket绑定到终端地址上
                listener.Bind(ipe);
                // 开始监听 最大允许处理1000个连接
                listener.Listen(1000);
                Console.WriteLine("开始监听");
                // 开始接受客户端请求 程序在这里会堵塞
                Socket mySocket=listener.Accept();

                byte[] bs = new byte[1024];
                int n = mySocket.Receive(bs);
                // 将客户端发来的数据返回给客户端
                mySocket.Send(bs);
                // 半闭与客户端的连接
                mySocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
