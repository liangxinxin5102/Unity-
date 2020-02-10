
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchUI : MonoBehaviour {
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || GUIUtility.hotControl != 0 )
            {
                Debug.Log("点击到UI");
            }
            else
            {
                Debug.Log("没有点击到UI");
            }
        }
    }
}
