using UnityEngine;

public class ShowAlertDemo : MonoBehaviour {
	void Start () {
        // 调用IOS代码显示对话框
		UnityIOSKit.ShowAlert ("Unity标题", "Unity内容");
	}
	// 在IOS点击OK按钮后返回的消息
	void OnButtonClick( string msg )
	{
		Debug.Log (msg); // 打印Log
	}
}
