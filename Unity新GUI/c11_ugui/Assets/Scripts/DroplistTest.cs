using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroplistTest : MonoBehaviour {

    public Dropdown dropdown;
	void Start () {
        
        dropdown.AddOptions(new List<string>() { "苹果", "香蕉" }); // 添加更多元素
        dropdown.onValueChanged.AddListener(delegate (int n)
        {
            Debug.Log(n); // 当前选择的元素，0表示第1个
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
