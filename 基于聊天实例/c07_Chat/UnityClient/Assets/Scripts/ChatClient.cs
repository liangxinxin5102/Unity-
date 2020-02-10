using UnityEngine;
using UnityEngine.UI;
using UnityNetwork;

public class ChatClient : MonoBehaviour {

    ChatManager clientPeer;

    public Text revTxt;  // 收到的聊天消息
    public Text inputText; // 输入的聊天消息
    public Button sendMsgButton; // 发送聊天消息按钮

    // 收到的聊天消息
    // public string revString = "";

    // 输入的聊天消息
    // protected string inputString = "";

    // Use this for initialization
    void Start () {

        // 在这里创建ChatManager实例
        clientPeer = new ChatManager();
        // 注册一个聊天消息
        clientPeer.AddHandler("chat", OnChat);
        clientPeer.Start();

        sendMsgButton.onClick.AddListener(delegate ()
        {
            SendChat();  // 点击按钮发送消息
        });
    }
	
	// Update is called once per frame
	void Update () {
        // unity中处理逻辑使用的是单线程
        clientPeer.Update();
	}
    /*
    void OnGUI()
    {
        // 显示收到的聊天记录
        GUI.Label(new Rect(5, 5, 200, 30), revString);

        // 输入聊在消息
        inputString = GUI.TextField(new Rect(Screen.width * 0.5f - 200, Screen.height * 0.5f - 20, 400, 40), inputString);

        // 发送聊天消息
        if ( GUI.Button( new Rect( Screen.width*0.5f-100,Screen.height*0.65f,200,30),"发送消息") )
        {
            // 向服务器发送聊天消息
            SendChat();
        }
    }
    */
    // 发送聊天消息
    void SendChat()
    {
        // 聊天数据包
        Chat.ChatProto proto = new Chat.ChatProto();
        proto.userName = "客户端";
        proto.chatMsg = inputText.text;

        NetPacket p = new NetPacket();
        p.BeginWrite("chat");
        p.WriteObject<Chat.ChatProto>(proto);
        p.EncodeHeader();

        clientPeer.Send(p);
        //清空输入框
        inputText.text = "";
    }

    // 处理聊天消息
    public void OnChat(NetPacket packet)
    {
        Debug.Log("收到服务器的消息");
        Chat.ChatProto proto = packet.ReadObject<Chat.ChatProto>();
        revTxt.text = proto.userName + ":" + proto.chatMsg;  // 显示收到的消息

    }


   

}
