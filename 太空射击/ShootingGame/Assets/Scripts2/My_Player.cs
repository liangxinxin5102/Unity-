using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Player : MonoBehaviour
{

    // 速度
    public float m_speed = 1;
    // 生命
    public int m_life = 3;

    // 子弹prefab
    public Transform m_rocket;
    protected Transform m_transform;

    // 发射子弹计时器
    float m_rocketTimer = 0;

    public AudioClip m_shootClip;  // 声音
    protected AudioSource m_audio;  // 声音源
    public Transform m_explosionFX;  // 爆炸特效

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

        m_rocketTimer -= Time.deltaTime;
        if (m_rocketTimer <= 0)
        {
            m_rocketTimer = 0.1f;

            // 按空格键或鼠标左键发射子弹
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);

                // 播放射击声音
                m_audio.PlayOneShot(m_shootClip);
            }
        }

    }
}
