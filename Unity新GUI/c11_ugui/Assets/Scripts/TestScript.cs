using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 注意使用Unity UI要引入该命名空间

public class TestScript : MonoBehaviour {

    public Text textObject;  // 控件
    void Start()
    {
        textObject.text = "Unity3D手机游戏开发";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
