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
// 로컬에서 월드로 변환하는거 필요없어보여도 필요하네
// 다음은 마우스 이동으로 시네머신 스크린값 바꾸기 해야지 // ScreenX값은 처음만 작동 한다네 취소
// 아니면 시네머신으로 스킬쓸시 화면 줌인 하는거 찾아보든가
// 네비랑 키보드 이동 같이 사용 못하겠다 // 오브젝트 밖으러 나가는건 사용할 수 있을 듯
