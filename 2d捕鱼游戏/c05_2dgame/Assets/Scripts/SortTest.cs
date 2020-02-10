using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer r = this.GetComponent<SpriteRenderer>();
        r.sortingLayerName = "Test Layer";
        r.sortingOrder = 100;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
