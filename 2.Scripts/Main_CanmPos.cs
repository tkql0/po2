using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_CanmPos : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CamPos;

    private void Awake()
    {
        CamPos = this.transform.position;
        // 시네머신카메라에 할당할 오브젝트가 없다면 처음 카메라 위치로 되돌릴 포지션 저장
    }
}
