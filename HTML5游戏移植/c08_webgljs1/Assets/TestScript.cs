using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.ExternalCall("sayHello", "我是Unity!");
	}

    void onSayHello(string message)  // 由Javascript发回来的回调函数
    {
        Debug.Log(message);
    }
}
