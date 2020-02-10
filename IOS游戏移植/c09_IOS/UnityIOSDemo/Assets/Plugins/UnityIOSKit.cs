using UnityEngine;
using System.Runtime.InteropServices;

public static class UnityIOSKit{
	
    // 导入.m文件中的C函数
	[DllImport ("__Internal")]
	private static extern void ShowWarningBox(string strTitle ,string strText);

    // 将在Unity中使用的静态函数
	static public void ShowAlert(string strTitle ,string strText)
	{
		if ( Application.platform==RuntimePlatform.IPhonePlayer) // 只在Iphone平台调用
		{
			ShowWarningBox( strTitle , strText); // 调用.m文件中的C函数
		}
	}
}
