using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class DragTest : MonoBehaviour {

    public RectTransform dragArea;
    public Image imageDrag;
    public Image imageTarget;
    
    void Start () {
        EventTrigger trigger = imageDrag.gameObject.AddComponent<EventTrigger>(); // 获取EventTrigger组件

        EventTrigger.Entry dragentry = new EventTrigger.Entry();
        dragentry.eventID = EventTriggerType.Drag;  // 创建一个拖动事件
        dragentry.callback = new EventTrigger.TriggerEvent();
        dragentry.callback.AddListener(delegate (BaseEventData eventData) {
            Vector2 touchpos = ((PointerEventData)eventData).position; //获得当前拖动的屏幕坐标位置
            Vector2 uguiPos;
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dragArea, touchpos, ((PointerEventData)eventData).enterEventCamera, out uguiPos); // 将屏幕坐标转换为ugui本地坐标
            Debug.Log(isRect); 
            if (isRect && RectTransformUtility.RectangleContainsScreenPoint(dragArea, touchpos, ((PointerEventData)eventData).enterEventCamera) )  // 如果拖动位置在区域内
                imageDrag.rectTransform.localPosition = uguiPos;  // 更新被拖动图像的位置
            imageDrag.raycastTarget = false;  // 拖动的时候，防止阻挡射线探测
        });

        EventTrigger.Entry enddragentry = new EventTrigger.Entry();
        enddragentry.eventID = EventTriggerType.EndDrag;  // 结束拖动事件
        enddragentry.callback = new EventTrigger.TriggerEvent();
        enddragentry.callback.AddListener(delegate (BaseEventData eventData) {
            var go = ((PointerEventData)eventData).pointerEnter;
            if (go != null && go.name.CompareTo("Image Target") == 0)  // 如果拖动到目标位置
            {
                imageDrag.rectTransform.position = imageTarget.rectTransform.position;
            }
            imageDrag.raycastTarget = true;
        });

        trigger.triggers.Add(dragentry);
        trigger.triggers.Add(enddragentry);

    }
	
}
