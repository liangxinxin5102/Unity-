using UnityEngine;
public class CameraPoint : MonoBehaviour 
{
    public static CameraPoint Instance = null;
    void Awake(){
        Instance = this;
    }
    // �ڱ༭������ʾһ��ͼ��
    void OnDrawGizmos(){
        Gizmos.DrawIcon(transform.position, "CameraPoint.tif");
    }
}
