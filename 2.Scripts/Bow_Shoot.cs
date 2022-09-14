using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bow_Shoot : MonoBehaviour
{
    Player player;

    public GameObject Aiming_Point;

    public CinemachineVirtualCamera Cincamera;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            Aiming_Point.SetActive(true);
        }
        else
        {
            Aiming_Point.SetActive(false);
        }
    }
}
