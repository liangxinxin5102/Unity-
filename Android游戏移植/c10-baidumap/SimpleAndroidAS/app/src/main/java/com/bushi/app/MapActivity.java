package com.bushi.app;
import android.app.Activity;
import android.os.Bundle;

import com.baidu.mapapi.map.*;
import com.baidu.mapapi.model.LatLng;

public class MapActivity extends Activity {

    MapView mMapView = null;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);



        //获取地图控件引用
        mMapView = (MapView) findViewById(R.id.bmapView);
        BaiduMap baidumap = mMapView.getMap();

        //定义文字所显示的坐标点
        //LatLng llText = new LatLng(39.963175, 116.400244);  // 北京
        LatLng llText = new LatLng(31.203076, 121.475282);  // 上海
        //构建文字Option对象，用于在地图上添加文字
        OverlayOptions textOption = new TextOptions()
                .bgColor(0xAAFFFF00)
                .fontSize(35)
                .fontColor(0xFFFF00FF)
                .text("卜石艺术馆")
                .rotate(0)
                .position(llText);
        //在地图上添加该文字对象并显示
        baidumap.addOverlay(textOption);

        MapStatus mapStatus = new MapStatus.Builder()
                .target(llText)
                .zoom(18)
                .build();

        MapStatusUpdate mapStatusUpdate = MapStatusUpdateFactory.newMapStatus(mapStatus);
        //MapStatusUpdate mapStatusUpdate = MapStatusUpdateFactory.newLatLng(llText);
        baidumap.setMapStatus(mapStatusUpdate);
    }

    // Quit Unity
    @Override protected void onDestroy ()
    {
        super.onDestroy();
        mMapView.onDestroy();
    }

    // Pause Unity
    @Override protected void onPause()
    {
        super.onPause();
        mMapView.onPause();
    }

    // Resume Unity
    @Override protected void onResume()
    {
        super.onResume();
        mMapView.onResume();
    }
}
