using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Shoot : MonoBehaviour
{
    public GameObject Aiming_Point;

    public Camera_Target camera_target;
    public Transform Aiming_target;

    public GameObject Shoot_obj;

    float rotSpeed = 5.0f;

    LineRenderer Line;

    Vector3 startPos, endPos;

    Player player;

    private void Start()
    {
        camera_target = GameManager.Instance.Camera_target;
        Aiming_target = this.transform.GetChild(0);
        Line = GetComponent<LineRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(GameManager.Instance.point.PointHealth >= 0 && camera_target.Main_Cincamera.enabled != true)
            Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            float MouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * rotSpeed * MouseX);
            // 마우스가 좌우로 움직이면 움직인 방향으로 회전
            Aiming_Point.SetActive(true);
            float MouseY = Input.GetAxis("Mouse Y");
            Aiming_target.transform.Translate(Vector3.forward * MouseY);
            // 마우스를 앞뒤로 움직인만큼 이동
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                for (int i = 0; i < Line.positionCount; i++)
                {
                    Line.SetPosition(i, Vector3.zero);
                }
            }

            if (player.inbow == true)
                line_renderer();
        }

        else if(Input.GetMouseButtonUp(0))
        {
            Aiming_Point.SetActive(false);

            if (player.inbow == true)
                Shoot_Obj_Spawn();

            for (int i = 0; i < Line.positionCount; i++)
            {
                Line.SetPosition(i, Vector3.zero);
            }

            Aiming_target.localPosition = new Vector3(0, 1.3f, 3);
        }
    }

    void Shoot_Obj_Spawn()
    {
        GameObject Attack = Instantiate(Shoot_obj, transform.position + new Vector3 (endPos.x * 2 , 1, endPos.z * 2), Quaternion.identity);
        Destroy(Attack, 2f);
    }

    void line_renderer()
    {
        startPos = this.transform.position;
        endPos = Aiming_target.position;

        Vector3 center = (startPos + endPos) * 0.5f;
        center.y -= 3;

        startPos = startPos - center;
        endPos = endPos - center;

        for (int i = 0; i < Line.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(Line.positionCount - 1));

            point += center;
            Line.SetPosition(i, point);
        }
    }
}
