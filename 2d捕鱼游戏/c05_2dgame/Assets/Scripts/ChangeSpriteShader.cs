using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteShader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        render.material.shader = Shader.Find("Sprites/Gray");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
