using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageFilled : MonoBehaviour {

    public Image image;
	// Use this for initialization
	void Start () {
        image.fillAmount = 0.5f; // 
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
