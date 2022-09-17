using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bow_Shoot : MonoBehaviour
{
    Player player;

    public GameObject Aiming_Point;

    public Camera_Target camera_target;
    public Person_Cam person_cam;

    float rotSpeed = 10.0f;

    private void Start()
    {
        player = GetComponent<Player>();
        camera_target = GameManager.Instance.Camera_target;
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && camera_target.MainCam.GetComponent<CinemachineVirtualCamera>().enabled != true)
        {
            person_cam.Cincamera.Follow = camera_target.Cincamera.Follow;
            float MouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * rotSpeed * MouseX);
            camera_target.GetComponent<CinemachineVirtualCamera>().enabled = false;
            person_cam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            Aiming_Point.SetActive(true);
        }
        else if(!Input.GetMouseButton(0) && camera_target.MainCam.GetComponent<CinemachineVirtualCamera>().enabled != true)
        {
            person_cam.GetComponent<CinemachineVirtualCamera>().enabled = false;
            camera_target.GetComponent<CinemachineVirtualCamera>().enabled = true;
            Aiming_Point.SetActive(false);
        }
    }
}
