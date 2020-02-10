using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggle : MonoBehaviour {

    Toggle toggle;

	// Use this for initialization
	void Start () {
        toggle.onValueChanged.AddListener(delegate (bool isOn) {
            if ( isOn )
            {
                Debug.Log("toggle on");  
            }
            else
            {
                Debug.Log("toggle off");
            }
        });
    }
}
