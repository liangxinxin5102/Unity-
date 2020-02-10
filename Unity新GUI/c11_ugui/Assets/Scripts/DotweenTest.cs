using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    public Image image;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;  // 将TimeScale设为0
        image.transform.DOMove(new Vector3(-1000, 0, 0), 1.0f).From(true).SetUpdate(true);  // SetUpdate设为true将不受TimeScale影响
        image.DOFade(0,1.0f).From().SetUpdate(true).OnComplete(OnAnimationFinish);
        
        //image.transform.DOPause();  // 暂停位移动画
        //image.DOPause();  // 暂停渐变动画
        //image.DOKill();  // 取消动画

        DOVirtual.Float(0, 100, 1, OnParamUpdate).SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Restart); // 动画由0到100的值，并采用动画曲线，始终循环
    }

    // Update is called once per frame
    void OnAnimationFinish()
    {
        Debug.Log("动画播放结束");
    }

    void OnParamUpdate( float f )
    {
        Debug.Log(f);
    }
}
