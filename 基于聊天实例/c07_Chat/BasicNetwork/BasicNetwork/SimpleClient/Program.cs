using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SimpleClient
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
                Socket client = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // 开始连接服务器，程序在这里会堵塞
                client.Connect(ipe);
                Console.WriteLine("连接到服务器");
                //向服务器发送数据
                string data = "hello,world";
                byte[] bs=UTF8Encoding.UTF8.GetBytes(data);
                client.Send(bs);
                // 用一个数组保存服务器返回的数据
                byte[] rev = new byte[256];
                // 接收到服务器返回的数据
                client.Receive(rev);
                Console.WriteLine(UTF8Encoding.UTF8.GetString(rev));
                // 关闭连接
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
