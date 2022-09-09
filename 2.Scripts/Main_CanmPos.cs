using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_CanmPos : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CamPos;
    // 시네머신카메라가 비활성화 된 후 타켓이 없을 때 위치를 되돌리기 위해 공개 설정
    // 바뀌지 않기 때문에 숨김(?) 설정
    // 자식 스크립트로 만들면 되려나? X

    private void Awake()
    {
        CamPos = this.transform.position;
        // 시네머신카메라에 할당할 오브젝트가 없다면 처음 카메라 위치로 되돌릴 포지션 저장
    }
}
