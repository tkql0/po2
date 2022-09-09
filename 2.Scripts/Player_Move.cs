using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class Player_Move : MonoBehaviour
{
    void Update()
    {
        if (isMove == true)
        {
            PlayerMove();
        }
    }

    public Transform Cam;
    public Transform Player;

    float speed = 10;

    float hAxis;
    float vAxis;

    public Vector3 move;

    Animator anim;

    bool ShiftDown;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public bool isMove;

    void PlayerMove()
    {
        Player player = gameObject.GetComponent<Player>();
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        ShiftDown = Input.GetButton("Walk");

        move = Vector3.right * hAxis + Vector3.forward * vAxis;

        move = Camera.main.transform.TransformDirection(move).normalized;

        if (player.inbow != true && player.inshild != true)
        {
            transform.position += move * speed * (ShiftDown ? 0.5f : 0.7f) * Time.deltaTime;
            anim.SetBool("isBowWalk", false);
            anim.SetBool("isShildWalk", false);
        }

        else
        {
            transform.position += move * speed * 0.5f * Time.deltaTime;

            if (player.inbow == true)
            {
                anim.SetBool("isBowWalk", move != Vector3.zero);
                anim.SetBool("isShildWalk", false);
            }

            if (player.inshild == true)
            {
                anim.SetBool("isBowWalk", false);
                anim.SetBool("isShildWalk", move != Vector3.zero);
            }
        }

        //transform.LookAt(transform.position + move);
        if (move != Vector3.zero)
        {
            Vector3 relativePos = (transform.position + move) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
        }

        anim.SetBool("isRun", move != Vector3.zero);
        anim.SetBool("isWalk", ShiftDown);
    }
}
// ���ÿ��� ����� ��ȯ�ϴ°� �ʿ������� �ʿ��ϳ�
// ������ ���콺 �̵����� �ó׸ӽ� ��ũ���� �ٲٱ� �ؾ��� // ScreenX���� ó���� �۵� �Ѵٳ� ���
// �ƴϸ� �ó׸ӽ����� ��ų���� ȭ�� ���� �ϴ°� ã�ƺ��簡
// �׺�� Ű���� �̵� ���� ��� ���ϰڴ� // ������Ʈ ������ �����°� ����� �� ���� ��
