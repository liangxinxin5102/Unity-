using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    /// <summary>
    /// 是否碰到球
    /// </summary>
    bool m_isHit = false;
    /// <summary>
    /// 碰到球的位置
    /// </summary>
    Vector3 m_startPos;
    /// <summary>
    /// 球的渲染器
    /// </summary>
    SpriteRenderer m_spriteRenderer;

	// Use this for initialization
	void Start () {

        m_spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        // 如果点击鼠标左键并且碰到球
        if (Input.GetMouseButtonDown(0) && IsHit() )
        {
            // 记录位置
            m_startPos = Input.mousePosition;
        }

        //当松开鼠标
        if (Input.GetMouseButtonUp(0) && m_isHit)
        {
            Vector3 endPos = Input.mousePosition;

            Vector3 v = (m_startPos - endPos) * 3.0f;
            // 将body type设为Dynamic
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            // 向球加一个力
            this.GetComponent<Rigidbody2D>().AddForce(v);

        }
	}

    bool IsHit()
    {
        m_isHit = false;
        // 获得鼠标位置
        Vector3 ms = Input.mousePosition;
        // 将鼠标位置由屏幕坐标转为世界坐标
        ms = Camera.main.ScreenToWorldPoint(ms);
        // 获得球的位置
        Vector3 pos = this.transform.position;
        // 获得球Sprite的宽和高（注意宽和高不是图片像素值的宽和高）
        float w = m_spriteRenderer.bounds.extents.x;
        float h = m_spriteRenderer.bounds.extents.y;
        // 判断鼠标的位置是否是在Sprite的矩形范围内
        if (ms.x > pos.x - w && ms.x < pos.x + w &&
            ms.y > pos.y - h && ms.y < pos.y + h)
        {
            m_isHit = true;
            return true;
        }

        return m_isHit;
    }

}
