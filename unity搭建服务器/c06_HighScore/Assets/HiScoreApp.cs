using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HiScoreApp : MonoBehaviour {

    // 按钮位置
    public Rect m_uploadBut;
    public Rect m_downLoadBut;

    // 输入框位置
    public Rect m_nameLablePos;
    public Rect m_scoreLablePos;
    public Rect m_nameTxtField;
    public Rect m_scoreTxtField;

    // 滑动框位置
    public Rect m_scrollViewPosition;
    public Vector2 m_scrollPos;
    public Rect m_scrollView;

    // 网格位置
    public Rect m_gridPos;

    public string[] m_hiscores;

    // 用户名
    protected string m_name="";

    // 得分
    protected string m_score = "";

    public bool useRedis = false;

	// Use this for initialization
	void Start () {

        m_hiscores = new string[20]; // 列出20个排行榜名单
	}

    // 创建UI
    void OnGUI()
    {
        GUI.Label(m_nameLablePos, "用户名");
        GUI.Label(m_scoreLablePos, "得分");
        m_name =GUI.TextField(m_nameTxtField, m_name);  // 名字输入框
        m_score = GUI.TextField(m_scoreTxtField, m_score);  // 密码输入框
       
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;  // 设置文字对齐

        // 上传分数按钮
        if (GUI.Button(m_uploadBut, "上传"))
        {
            StartCoroutine(UploadScore(m_name, m_score)); // HTTP请求
            m_name = "";  // 清空UI中的文本输入
            m_score = "";  // 清空UI中的文本输入
        }

        // 下载分数按钮
        if (GUI.Button(m_downLoadBut, "下载"))
        {
            if (!useRedis)
                StartCoroutine(DownloadScores(m_name, m_score));  // MySQL请求
            else
                StartCoroutine(DownloadScoresRedis(m_name, m_score));  // Redis请求
        }


        GUI.skin.button.alignment = TextAnchor.MiddleLeft;  // 设置文字对齐

        m_scrollPos =GUI.BeginScrollView(m_scrollViewPosition, m_scrollPos, m_scrollView);

        m_gridPos.height = m_hiscores.Length * 30;

        // 显示分数排行榜
        GUI.SelectionGrid(m_gridPos, 0, m_hiscores, 1);

        GUI.EndScrollView();
    }

    // 上传分数
    IEnumerator UploadScore( string name, string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);

        WWW www = new WWW("http://127.0.0.1/uploadscore.php", form );

        yield return www;

        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
            Debug.Log(www.text);
    }

    //下载排行榜
    IEnumerator DownloadScores(string name, string score)
    {
        WWW www = new WWW("http://127.0.0.1/downloadscores.php");

        yield return www;

        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.text);
            // 将PHP返回的数据解析为字典格式
            var dict = MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;
            int index = 0;
            foreach (object v in dict.Values)
            {
                UserData user = new UserData(); ;
                MiniJSON.Json.ToObject(user, v);  // 将字典中的值反序列化为UserData

                // 更新UI上的文字
                m_hiscores[index] = string.Format( "ID:{0:D2}   名字:{1}   分数{2}",  user.id,user.name,user.score);
                index++;
            }
        }
    }

    // 下载排行榜（Redis版本）
    IEnumerator DownloadScoresRedis(string name, string score)
    {
        WWW www = new WWW("http://127.0.0.1/downloadscores_redis.php");

        yield return www;

        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.text);
            // 将PHP返回的数据解析为字典格式
            var dict = MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;
            int index = 0;
            foreach (object v in dict.Values)
            {
                UserData user = new UserData(); ;
                MiniJSON.Json.ToObject(user, v);  // 将字典中的值反序列化为UserData

                // 更新UI上的文字
                m_hiscores[index] = string.Format("ID:{0:D2}   名字:{1}   分数{2}", user.id, user.name, user.score);
                index++;
            }
        }
    }

    // 定义一个类，它的字段名称一定要与服务器返回的JSON格式数据的键名一致
    public class UserData
    {
        public int id;
        public string name;
        public int score;
    }
}
