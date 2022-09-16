using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Person_Cam : MonoBehaviour
{
    public CinemachineVirtualCamera Cincamera;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
    }
}
