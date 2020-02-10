using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetBuilder : MonoBehaviour {

    [MenuItem("Assets/Get AssetBundle names")]
    static void GetAssetBundleNames()  // 获取所有AssetBundle名称
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
        {
            Debug.Log("AssetBundle: " + name); // 显示全部AssetBundle的名称
        }
    }

    [MenuItem("Assets/Build Asset Bundles")]
    static void BuildAssets()
    {
        // 在目标路径创建AssetBundle
        BuildPipeline.BuildAssetBundles("Assets/Bundles/Win", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Assets/Build Asset Bundles Auto")]
    static void BuildAssetsAuto()
    {
        // 如果输出路径不存在则创建相应目录
        if (!System.IO.Directory.Exists("Assets/Bundles/Win")){
            System.IO.Directory.CreateDirectory("Assets/Bundles/Win");
        }
        // 查找路径下的所有.FBX和.prefab文件
        var filelist = System.IO.Directory.GetFiles("Assets/Prefabs", "*.*", System.IO.SearchOption.AllDirectories).Where(s => s.EndsWith(".prefab") || s.EndsWith(".FBX"));

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = "planes"; // 设置AssetBundle名称
        buildMap[0].assetNames = filelist.ToArray();  // 将.FBX和.prefab文件添加到AssetBundle中
        foreach (string asset in buildMap[0].assetNames)
            Debug.Log(asset);

        // 打包AssetBundle
        BuildPipeline.BuildAssetBundles("Assets/Bundles/Win", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

    }

}
