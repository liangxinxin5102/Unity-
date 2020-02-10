using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {

    public Slider slider;
	void Start () {
        slider.onValueChanged.AddListener(delegate (float v)
        {
            Debug.Log(v);
        });
    }

    void Update()
    {
        slider.value += 0.1f * Time.deltaTime;
    }
}
