using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_CanmPos : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CamPos;

    private void Awake()
    {
        CamPos = this.transform.position;
        // �ó׸ӽ�ī�޶� �Ҵ��� ������Ʈ�� ���ٸ� ó�� ī�޶� ��ġ�� �ǵ��� ������ ����
    }
}
