using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;
    // Ÿ���� Ž���� �Ÿ�

    public GameObject Danger;

    public LayerMask targetMask;
    // Ÿ�ٰ� ��ֹ�

    private void Update()
    {
        Search();
    }

    void Search()
    {
        // ����� Ÿ���� �ʱ�ȭ
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Player_Seatch_ange, targetMask);
        // �����ȿ� ���� Ÿ�ٷ��̾ ����

        if(inTarget.Length >= 1)
            Danger.SetActive(true);

        else
            Danger.SetActive(false);
    }
}
