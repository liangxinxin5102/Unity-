using UnityEngine;
using System.Runtime.InteropServices;

public static class UnityIOSKit{
	
    // ����.m�ļ��е�C����
	[DllImport ("__Internal")]
	private static extern void ShowWarningBox(string strTitle ,string strText);

    // ����Unity��ʹ�õľ�̬����
	static public void ShowAlert(string strTitle ,string strText)
	{
		if ( Application.platform==RuntimePlatform.IPhonePlayer) // ֻ��Iphoneƽ̨����
		{
			ShowWarningBox( strTitle , strText); // ����.m�ļ��е�C����
		}
	}
}
