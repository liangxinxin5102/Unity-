using UnityEngine;
using System.Collections;
using UnityNetwork;

public class ChatManager : NetworkManager {

    TCPPeer client;

	// Use this for initialization
	public void Start () {
        // 连接到服务器
        client = new TCPPeer(this);
        client.Connect("127.0.0.1", 10001);
	}

    // 发送聊天消息
    public void Send( NetPacket packet )
    {
        client.Send(client.socket, packet);
    }

    // 处理丢失连接
    public override void OnLost(NetPacket packet)
    {
        Debug.Log("丢失与服务器的连接");
    }

    // 处理客户端取得与服务器的连接
    public override void OnConnected(NetPacket packet)
    {
        Debug.Log("成功连接到服务器");
    }

    // 处理客户端与服务器连接失败
    public override void OnConnectFailed(NetPacket packet)
    {
        Debug.Log("连接服务器失败，请退出");
    }

}// end file

