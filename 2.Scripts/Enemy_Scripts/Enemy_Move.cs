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
        // ���� ���ϸ� �ڷ�ƾ���� bool�� �ٲٱ�
        if(isMove == true)
            // �������� ������ �ʾҴٸ�
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
