using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTest : MonoBehaviour {

    public ScrollRect scrollrect;
	void Start () {
        scrollrect.onValueChanged.AddListener(delegate (Vector2 v)
        {
            Debug.Log(v); // 0 表示拉到最下面，1最上面
        });
        scrollrect.verticalNormalizedPosition = 0.5f; // 手工移动列表，0.5f表示移到中间
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
