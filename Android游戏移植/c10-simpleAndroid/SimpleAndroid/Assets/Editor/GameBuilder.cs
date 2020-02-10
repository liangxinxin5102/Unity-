using UnityEngine;
using UnityEditor;
public class GameBuilder
{
    // 设置输出路径, 默认根目录为Assets这一级目录
    private const string BuildPath = "../export project/";

    [MenuItem("GameBuilder/Build Android")]
    public static void BuildForAndroid()
    {
        // 如果不是android平台,转为android平台
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget( BuildTargetGroup.Android, BuildTarget.Android);
        }

        // 检查输出路径
        if (!System.IO.Directory.Exists(BuildPath))
        {
            System.IO.Directory.CreateDirectory(BuildPath);
        }
        // 设置标识符
        PlayerSettings.applicationIdentifier = "com.demo.simpleandroid";
        // 允许读写SD卡
        PlayerSettings.Android.forceSDCardPermission = true;
        // 可以使用脚本自动设置签名信息
        // 如果是使用Export Android工程而不是APK，这一步不是必须的
        //PlayerSettings.Android.keystoreName = "user.keystore";
        //PlayerSettings.Android.keystorePass = "123456";
        //PlayerSettings.Android.keyaliasName = "u2e demo";
        //PlayerSettings.Android.keyaliasPass = "123456";
        // 添加一个预编译变量（这里只是举例）
        // 在不同平台切换时非常有用, 针对不同的平台,可以使用不同的预编译变量，比如这里添加一个TEST
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "TEST");

        // 输出!
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scene/demo.unity", };  // 需要构建的场景
        buildPlayerOptions.locationPathName = BuildPath;  // 输出路径
        buildPlayerOptions.target = BuildTarget.Android;  // 设置输出平台
        buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;  // 导出Android工程选项
        BuildPipeline.BuildPlayer(buildPlayerOptions);  // 导出

        Debug.Log("done");
    }
}
