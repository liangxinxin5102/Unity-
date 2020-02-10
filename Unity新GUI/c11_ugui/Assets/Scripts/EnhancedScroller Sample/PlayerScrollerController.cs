using UnityEngine;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class PlayerScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<PlayerData> _data;  // 一个列表，存储所有的PlayerData数据
    public EnhancedScroller myScroller;  // Scroller UI控件
    public PlayerCellView playerCellViewPrefab;  // 列中元素UI的Prefab

    void Start()
    {
        _data = new List<PlayerData>();  // 示例简单加入一些数据
        _data.Add(new PlayerData() { mName = "Lion", mSex = "男" });
        _data.Add(new PlayerData() { mName = "Bear", mSex = "女" });
        _data.Add(new PlayerData() { mName = "Eagle", mSex = "男" });
        _data.Add(new PlayerData() { mName = "Dolphin", mSex = "女" });
        _data.Add(new PlayerData() { mName = "Ant", mSex = "男" });
        _data.Add(new PlayerData() { mName = "Ant", mSex = "男" });
        _data.Add(new PlayerData() { mName = "Ant", mSex = "男" });
        _data.Add(new PlayerData() { mName = "Ant", mSex = "男" });
        myScroller.Delegate = this;  // 设置回调目标
        myScroller.ReloadData();  // 刷新载入的数据
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;  // 返回元素数量
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;  // 返回UI元素的高度，这个值要根据实际UI的大小来设置
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // 在这里加载列表中的UI元素，初始化，设置按钮事件回调等，它们将是被循环使用的
        PlayerCellView cellView = scroller.GetCellView(playerCellViewPrefab) as PlayerCellView;
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
}
