using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    Animator anim;

    bool isMove;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // 공격 당하면 코루틴으로 bool값 바꾸기
        if(isMove == true)
            // 움직임이 멈추지 않았다면
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);
        //Move();
    }

    IEnumerator Move()
    {
        isMove = true;
        yield return new WaitForSeconds(1);
    }
}
