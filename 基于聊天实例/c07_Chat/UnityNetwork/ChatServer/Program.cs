using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityNetwork;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();

            server.StartServer("127.0.0.1", 10001);
        }

        public class ChatServer : NetworkManager
        {
            // 保存所有的客户端连接
            List<Socket> peerList;

            // 服务器
            TCPPeer server;

            public ChatServer()
            {
                // 创建一个列表保存每个客户端的Socket
                peerList = new List<Socket>();
            }

            // 启动服务器
            public void StartServer( string ip , int port )
            {
                // 注册事件，本例中只有一个聊天消息
                AddHandler("chat", OnChat);

                server = new TCPPeer(this);
                server.Listen(ip, port);
                // 启动另一个线程处理逻辑消息
                this.StartThreadUpdate();
                Console.WriteLine("启动聊天服务器");
            }

            // 处理服务器接受客户端的连接
            public override void OnAccepted(NetPacket packet)
            {
                Console.WriteLine("接受新的连接");
                peerList.Add(packet.socket);
            }

            // 处理丢失连接
            public override void OnLost(NetPacket packet)
            {
                Console.WriteLine("丢失连接");
                peerList.Remove(packet.socket);
            }

            // 处理聊天消息
            public void OnChat(NetPacket packet)
            {
                // 在服务器上显示聊天内容
                Chat.ChatProto proto = packet.ReadObject<Chat.ChatProto>();
                if ( proto!=null )
                    Console.WriteLine(proto.userName + ":" + proto.chatMsg);

                packet.BeginWrite("chat");
                packet.WriteObject<Chat.ChatProto>(proto);
                packet.EncodeHeader();

                // 将消息转发给所有聊天客户端
                foreach (Socket sk in peerList)
                {
                    server.Send(sk, packet);
                }
              
                
            }
        }
    }
}
