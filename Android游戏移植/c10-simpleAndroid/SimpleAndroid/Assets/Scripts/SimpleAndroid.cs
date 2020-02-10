using UnityEngine;

public class SimpleAndroid : MonoBehaviour {

    // 响应按钮事件
    public void OnButtonClick()
    {
#if UNITY_ANDROID
        // 获取Android工程中的Activity对象
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

        string[] args = new string[2];
        args[0] = "Hello";
        args[1] = "World";

        // 调用将在Android Studio中定义的HelloWorld函数
        activity.Call("HelloWorld", args);
#endif
    }

    // 回调函数，该函将在Android Studio中的Java代码中触发
    void AndroidCallBack()
    {
        // 更改画面背景颜色
        Camera.main.backgroundColor = new Color(1, 0, 0);
    }
}
