using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField]
    float speed;

    float hAxis;
    float vAxis;

    Vector3 move;

    Animator Player_Anim;

    bool ShiftDown;
    public bool isMove;

    Player player;

    private void Start()
    {
        Player_Anim = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isMove == true)
            player_Move();
    }


    void player_Move()
    {
        if (!player.isDead)
        {
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");
            ShiftDown = Input.GetButton("Walk");

            move = Vector3.right * hAxis + Vector3.forward * vAxis;

            move = Camera.main.transform.TransformDirection(move).normalized;

            if (player.inbow != true && player.inshild != true)
            {
                transform.position += move * speed * (ShiftDown ? 0.5f : 0.7f) * Time.deltaTime;
                Player_Anim.SetBool("isBowWalk", false);
                Player_Anim.SetBool("isShildWalk", false);
            }

            else
            {
                transform.position += move * speed * 0.5f * Time.deltaTime;

                if (player.inbow == true)
                {
                    Player_Anim.SetBool("isBowWalk", move != Vector3.zero);
                    Player_Anim.SetBool("isShildWalk", false);
                }

                if (player.inshild == true)
                {
                    Player_Anim.SetBool("isBowWalk", false);
                    Player_Anim.SetBool("isShildWalk", move != Vector3.zero);
                }
            }

            if (move != Vector3.zero)
            {
                Vector3 relativePos = (transform.position + move) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
            }

            Player_Anim.SetBool("isRun", move != Vector3.zero);
            Player_Anim.SetBool("isWalk", ShiftDown);
        }
    }
}
