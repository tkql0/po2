using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;
    // 타겟을 탐색할 거리

    public GameObject Danger;

    public LayerMask targetMask;
    // 타겟과 장애물

    private void Update()
    {
        Search();
    }

    void Search()
    {
        // 저장된 타겟을 초기화
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Player_Seatch_ange, targetMask);
        // 범위안에 들어온 타겟레이어를 저장

        if(inTarget.Length >= 1)
            Danger.SetActive(true);

        else
            Danger.SetActive(false);
    }
}
