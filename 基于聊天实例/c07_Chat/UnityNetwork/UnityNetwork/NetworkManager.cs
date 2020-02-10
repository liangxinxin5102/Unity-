using System.Collections;
using System.Collections.Generic;

namespace UnityNetwork
{
    public class NetworkManager
    {
        // 逻辑处理使用一个独立线程，与网络的线程分开
        System.Threading.Thread myThread;

        /// <summary>
        /// 代理回调函数
        /// </summary>
        public delegate void OnReceive( NetPacket packet );

        // 每个消息对应一个OnReceive函数
        public Dictionary<string, OnReceive> handlers;

        // 存储网络数据的队列
        private Queue Packets = new Queue();

        public NetworkManager()
        {
            handlers = new Dictionary<string, OnReceive>();
            // 注册连接成功，丢失连接消息
            AddHandler("OnAccepted", OnAccepted);
            AddHandler("OnConnected", OnConnected);
            AddHandler("OnConnectFailed", OnConnectFailed);
            AddHandler("OnLost", OnLost);
        }

        // 注册网络消息
        // 将字符串形式的消息标识符和回调函数以键值对应的方式存入到一个Dictionary中
        public void AddHandler(string msgid, OnReceive handler)
        {
            handlers.Add(msgid, handler);
        }

        // 将数据包入队，然后在Update函数中使用GetPacket获得数据包。
        // 因为网络和逻辑处理有可能是在不同的线程中，所以在入队出队的时候使用了lock防止多线程带来的问题。
        public void AddPacket( NetPacket packet )
        {
            lock (Packets)
            {
                Packets.Enqueue(packet);
            }
        }

        // 数据包出队
        public NetPacket GetPacket()
        {
            lock (Packets)
            {
                if (Packets.Count == 0)
                    return null;
                return (NetPacket)Packets.Dequeue();
            }
        }

        // 开启一个新的线程运行ThreadUpdate函数，我们将在循环中检查是否有新的数据包入队，有则处理。
        public void StartThreadUpdate()
        {
            // 为逻辑部分建立新的线程
            myThread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadUpdate));
            myThread.Start();
        }

        // 逻辑线程
        protected void ThreadUpdate()
        {
              while (true)
              {
                  // 为了节约cpu, 每次循环暂停30帧
                  System.Threading.Thread.Sleep(30);
                  Update();
              }
        }

        public void Update()
        {
            NetPacket packet = null;
            for (packet = GetPacket(); packet != null; )
            {
                string msg = "";
                // 获得消息标识符
                packet.BeginRead(out msg);

                OnReceive handler = null;
                if (handlers.TryGetValue(msg, out handler))
                {
                    // 根据消息标识符找到相应的OnReceive代理函数
                    if (handler != null)
                        handler(packet);
                }
                // 继续获得其它的包
                packet = GetPacket();
            }
        }

        // 处理服务器接受客户端的连接
        public virtual void OnAccepted(NetPacket packet)
        {

        }

        // 处理客户端取得与服务器的连接
        public virtual void OnConnected(NetPacket packet)
        {

        }

        // 处理客户端取得与服务器连接失败
        public virtual void OnConnectFailed(NetPacket packet)
        {

        }

        // 处理丢失连接
        public virtual void OnLost(NetPacket packet)
        {

        }
    }
}
