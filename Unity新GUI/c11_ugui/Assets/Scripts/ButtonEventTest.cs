using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ButtonEventTest : MonoBehaviour {

    public Button button;

    void Start()
    {
       
        button.onClick.AddListener(OnButtonEvent);  // 方式1
        button.onClick.AddListener(delegate () // 方式2
        {
            // 这种方式可以很容易获取button按钮上的组件
            // var go = button.GetComponent<GameObject>();
            Debug.Log("button event2");
        });

        // button.onClick.RemoveAllListeners(); //清除按钮事件

        EventTrigger trigger = button.GetComponent<EventTrigger>(); // 获取EventTrigger组件
        //EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>(); // 或者使用脚本添加EventTrigger组件也可以
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;  // 设定为按压事件，同样的方式，还可以设定很多其它事件
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(delegate(BaseEventData eventData) { Debug.Log("On Press"); }); //按压事件回调
        trigger.triggers.Add(entry);

    }

    public void OnButtonEvent()
    {
        Debug.Log("button event");
    }

    public void OnPress(BaseEventData eventData)
    {
        
    }
}
