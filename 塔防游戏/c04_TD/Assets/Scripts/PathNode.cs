using UnityEngine;
public class PathNode : MonoBehaviour {

    public PathNode m_parent; // ǰһ���ڵ�
    public PathNode m_next; // ��һ���ڵ�

    // ������һ���ڵ�
    public void SetNext(PathNode node)
    {
        if (m_next != null)
            m_next.m_parent = null;
        m_next = node;
        node.m_parent = this;
    }
    // �ڱ༭������ʾ��ͼ��
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Node.tif");
    }
}
