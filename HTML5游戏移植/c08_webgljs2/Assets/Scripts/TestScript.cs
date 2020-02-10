using UnityEngine;
using System.Runtime.InteropServices;

public class TestScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void Hello(string str);

    // Use this for initialization
    void Start () {
        Hello("我是Unity");
    }

    void onSayHello(string message)  // 由Javascript发回来的回调函数
    {
        Debug.Log(message);
    }
}
