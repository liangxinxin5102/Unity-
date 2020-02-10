using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour {

    public InputField inputfield;
	void Start () {
        inputfield.onValueChanged.AddListener(delegate (string c)  // 输入内容变化
        {
            Debug.Log(c);
        });

        inputfield.onEndEdit.AddListener(delegate (string c)  // 输入结束
        {
            Debug.Log(c);
        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
