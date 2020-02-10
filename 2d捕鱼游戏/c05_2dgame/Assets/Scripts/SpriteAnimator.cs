
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    /// Sprite渲染器
    protected SpriteRenderer m_sprite;
    /// Sprite动画帧
    public Sprite[] m_clips;
    /// 动画计时器（默认每隔0.1秒更新一帧）
    protected float timer = 0.1f;
    /// 当前的帧数
    protected int m_frame = 0;

    void Start()
    {
        // 为当前GameObject添加一个Sprite渲染器
        m_sprite = this.gameObject.GetComponent<SpriteRenderer>();
        // 设置第1帧的Sprite
        m_sprite.sprite = m_clips[m_frame];
    }

    void Update()
    {
        // 更新时间
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.1f;
            // 更新帧
            m_frame++;
            if (m_frame >= m_clips.Length)
                m_frame = 0;
            // 更新Sprite动画帧
            m_sprite.sprite = m_clips[m_frame];
        }
    }
}
