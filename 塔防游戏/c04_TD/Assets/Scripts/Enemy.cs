using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour {

    public PathNode m_currentNode; // 敌人的当前路点
    public int m_life = 15;  // 敌人的生命
    public int m_maxlife = 15; // 敌人的最大生命
    public float m_speed = 2;  // 敌人的移动速度
    public System.Action<Enemy> onDeath;  // 敌人的死亡事件

    Transform m_lifebarObj;  // 敌人的UI生命条GameObject
    UnityEngine.UI.Slider m_lifebar; //控制生命条显示的Slider

	// Use this for initialization
	void Start () {

        GameManager.Instance.m_EnemyList.Add(this);

        // 读取生命条prefab
        GameObject prefab = (GameObject)Resources.Load("Canvas3D");
        // 创建生命条
        m_lifebarObj = ((GameObject)Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation, this.transform )).transform;
        m_lifebarObj.localPosition = new Vector3(0, 2.0f, 0);
        m_lifebarObj.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        m_lifebar = m_lifebarObj.GetComponentInChildren<UnityEngine.UI.Slider>();
        // 更新生命条位置和角度
        StartCoroutine(UpdateLifebar());
	}
	
	// Update is called once per frame
	void Update () {
        RotateTo();
        MoveTo();
	}

    // 转向目标
    public void RotateTo()
    {
        var position = m_currentNode.transform.position - transform.position;
        position.y = 0; // 保证仅旋转Y轴
        var targetRotation = Quaternion.LookRotation(position); // 获得目标旋转角度
        float next = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, //获得中间的旋转角度
            120 * Time.deltaTime);
        this.transform.eulerAngles = new Vector3(0, next, 0);
    }

    // 向目标移动
    public void MoveTo()
    {
        Vector3 pos1 = this.transform.position;
        Vector3 pos2 = m_currentNode.transform.position;
        float dist = Vector2.Distance(new Vector2(pos1.x,pos1.z),new Vector2(pos2.x,pos2.z));
        if (dist < 1.0f)
        {
            if (m_currentNode.m_next == null) // 没有路点，说明已经到达我方基地
            {
                GameManager.Instance.SetDamage(1); // 扣除一点伤害值
                DestroyMe();  // 销毁自身
            }
            else
                m_currentNode = m_currentNode.m_next;  // 更新到下一个路点
        }

        this.transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
        //m_bar.SetPosition(this.transform.position, 4.0f);
    }

    public void DestroyMe()
    {
        GameManager.Instance.m_EnemyList.Remove(this);
        onDeath(this);  // 发布消息
        Destroy(this.gameObject); // 注意在实际项目中一般不要直接调用Destroy
    }

    public void SetDamage(int damage)
    {
        m_life -= damage;
        if (m_life <= 0)
        {
            m_life = 0;
            // 每消灭一个敌人增加一些铜钱
            GameManager.Instance.SetPoint(5);
            DestroyMe();
        }
    }

    IEnumerator UpdateLifebar()
    {
        // 更新生命条的值
        m_lifebar.value = (float)m_life / (float)m_maxlife;
        // 更新角度，如终面向摄像机
        m_lifebarObj.transform.eulerAngles = Camera.main.transform.eulerAngles;
        yield return 0; // 没有任何等待
        StartCoroutine(UpdateLifebar());  // 循环执行
    }

}
