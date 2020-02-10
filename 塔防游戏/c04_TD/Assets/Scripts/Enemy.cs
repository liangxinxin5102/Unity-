using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour {

    public PathNode m_currentNode; // ���˵ĵ�ǰ·��
    public int m_life = 15;  // ���˵�����
    public int m_maxlife = 15; // ���˵��������
    public float m_speed = 2;  // ���˵��ƶ��ٶ�
    public System.Action<Enemy> onDeath;  // ���˵������¼�

    Transform m_lifebarObj;  // ���˵�UI������GameObject
    UnityEngine.UI.Slider m_lifebar; //������������ʾ��Slider

	// Use this for initialization
	void Start () {

        GameManager.Instance.m_EnemyList.Add(this);

        // ��ȡ������prefab
        GameObject prefab = (GameObject)Resources.Load("Canvas3D");
        // ����������
        m_lifebarObj = ((GameObject)Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation, this.transform )).transform;
        m_lifebarObj.localPosition = new Vector3(0, 2.0f, 0);
        m_lifebarObj.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        m_lifebar = m_lifebarObj.GetComponentInChildren<UnityEngine.UI.Slider>();
        // ����������λ�úͽǶ�
        StartCoroutine(UpdateLifebar());
	}
	
	// Update is called once per frame
	void Update () {
        RotateTo();
        MoveTo();
	}

    // ת��Ŀ��
    public void RotateTo()
    {
        var position = m_currentNode.transform.position - transform.position;
        position.y = 0; // ��֤����תY��
        var targetRotation = Quaternion.LookRotation(position); // ���Ŀ����ת�Ƕ�
        float next = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, //����м����ת�Ƕ�
            120 * Time.deltaTime);
        this.transform.eulerAngles = new Vector3(0, next, 0);
    }

    // ��Ŀ���ƶ�
    public void MoveTo()
    {
        Vector3 pos1 = this.transform.position;
        Vector3 pos2 = m_currentNode.transform.position;
        float dist = Vector2.Distance(new Vector2(pos1.x,pos1.z),new Vector2(pos2.x,pos2.z));
        if (dist < 1.0f)
        {
            if (m_currentNode.m_next == null) // û��·�㣬˵���Ѿ������ҷ�����
            {
                GameManager.Instance.SetDamage(1); // �۳�һ���˺�ֵ
                DestroyMe();  // ��������
            }
            else
                m_currentNode = m_currentNode.m_next;  // ���µ���һ��·��
        }

        this.transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
        //m_bar.SetPosition(this.transform.position, 4.0f);
    }

    public void DestroyMe()
    {
        GameManager.Instance.m_EnemyList.Remove(this);
        onDeath(this);  // ������Ϣ
        Destroy(this.gameObject); // ע����ʵ����Ŀ��һ�㲻Ҫֱ�ӵ���Destroy
    }

    public void SetDamage(int damage)
    {
        m_life -= damage;
        if (m_life <= 0)
        {
            m_life = 0;
            // ÿ����һ����������һЩͭǮ
            GameManager.Instance.SetPoint(5);
            DestroyMe();
        }
    }

    IEnumerator UpdateLifebar()
    {
        // ������������ֵ
        m_lifebar.value = (float)m_life / (float)m_maxlife;
        // ���½Ƕȣ��������������
        m_lifebarObj.transform.eulerAngles = Camera.main.transform.eulerAngles;
        yield return 0; // û���κεȴ�
        StartCoroutine(UpdateLifebar());  // ѭ��ִ��
    }

}
