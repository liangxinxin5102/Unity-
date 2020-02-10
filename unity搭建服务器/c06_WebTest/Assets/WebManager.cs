using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebManager : MonoBehaviour {

    string m_info = "无数据";

    public Texture2D m_uploadImage;

    protected Texture2D m_downloadTexture;

    protected AudioClip m_downloadClip;

	// Use this for initialization
	void Start () {
        StartCoroutine(DownloadSound());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.5f - 100, 500, 200), "");

       
        GUI.Label(new Rect(10, 10, 400, 30), m_info);

        if (m_downloadTexture != null)
        {
            GUI.DrawTexture(new Rect(0, 0, m_downloadTexture.width,
                m_downloadTexture.height), m_downloadTexture);
        }

        // 创建Get按钮
        if (GUI.Button(new Rect(10, 50, 150, 30), "Get Data"))
        {
            StartCoroutine(IGetData());
        }
        // 创建Post按钮
        if (GUI.Button(new Rect(10, 100, 150, 30), "Post Data"))
        {
            StartCoroutine(IPostData());
        }

        if (GUI.Button(new Rect(10, 150, 150, 30), "Request Image"))
        {
            StartCoroutine(IRequestPNG());
        }


        GUI.EndGroup();
    }

    IEnumerator IGetData()
    {
        WWW www = new WWW("http://127.0.0.1/Test.php?username=get&password=12345");

        yield return www;

        if (www.error != null)
        {
            m_info = www.error;
            yield return null;
        }

        m_info = www.text;
    }

    IEnumerator IPostData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "post");
        form.AddField("password", "6789");

        WWW www = new WWW("http://127.0.0.1/test.php", form);

        yield return www;

        if (www.error != null)
        {
            m_info = www.error;
            yield return null;
        }

        m_info = www.text;
    }

    IEnumerator IRequestPNG()
    {
        byte[] bs = m_uploadImage.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("picture", bs, "screenshot", "image/png");

        WWW www = new WWW("http://127.0.0.1/test.php", form);

        yield return www;

        if (www.error != null)
        {
            m_info = www.error;
            yield return null;
        }

        m_downloadTexture = www.texture;

    }

    IEnumerator DownloadSound()
    {
        // 请求下载声音文件
        WWW www = new WWW("http://127.0.0.1/music.wav");

        yield return www;

        if (www.error != null)
        {
            m_info = www.error;
            yield return null;
        }
        // 获得下载的声音文件
        m_downloadClip = www.GetAudioClip(false);

        // 播放声音
        GetComponent<AudioSource>().PlayOneShot(m_downloadClip);

    }

}
