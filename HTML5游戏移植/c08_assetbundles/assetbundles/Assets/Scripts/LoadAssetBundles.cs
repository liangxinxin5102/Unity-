using System.Collections;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(StartLoad());
	}

    IEnumerator StartLoad()
    {
        while (!Caching.ready)  // 检查缓存
            yield return null;

        int version = 1;  // 资源版本
        // 如果没有缓存该资源或版本号小于资源版本号，则下载该资源
        WWW www = WWW.LoadFromCacheOrDownload("http://localhost/Bundles/Win/planes", version); 

        // 等待下载完成
        yield return www;

        // 获得下载的AssetBundle, 包括plane1和plane2
        AssetBundle bundle = www.assetBundle;

        // 通过名称读取并创建AssetBundle中的资源
        Instantiate(bundle.LoadAsset("plane1"), new Vector3( 15, 0, 0), Quaternion.identity);

        // 通过名称异步读取AssetBundle中的资源
        AssetBundleRequest request = bundle.LoadAssetAsync("plane2", typeof(GameObject));

        // 等待异步读取完成
        yield return request;

        // 创建实例
        Instantiate(request.asset as GameObject);

        // 卸载AssetBundle
        bundle.Unload(false);

        // 释放网络请求的内存
        www.Dispose();
    }
}
