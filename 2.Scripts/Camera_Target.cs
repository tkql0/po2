using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Target : MonoBehaviour
{
    public CinemachineVirtualCamera Cincamera;
    //public CinemachineFreeLook FreeLook;

    public Player_Spawn Spawn_List;

    public GameObject MainCam;

    public int i = 0;
    // 현재 순서

    int ListIndex = 0;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i++;
        }
        if (i >= Spawn_List.UnitList.Count)
        {
            GetComponent<CinemachineVirtualCamera>().enabled = false;
            // 타겟을 보는 시네머신은 끄고
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            // 맵을 보는 시네머신은 키고
            i = 0;
            ListIndex = 0;
            // 현재 순서를 처음으로 되돌리기
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
            // 순서가 바뀐 뒤 카메라 타겟 설정
        }
        KeyDownTab();
    }


    public void KeyDownTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        { // Tab을 누르면 시네머신 카메라가 타겟을 저장된 스폰 리스트 순서대로 시점 변경
            if (Spawn_List.UnitList.Count == 0)
                return;
            if (Cincamera.Follow != null)
            { // 타겟이 있을 때만 실행
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = false;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = false;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = false;
                // 현재 순서가 바뀌기 전에 스크립트 끄기
                i++;
                // 모든 순서가 끝난 후 현재 순서 +1
                if (i >= Spawn_List.UnitList.Count)
                { // 현재 순서가 스폰된 수보다 클 경우
                    StartCoroutine(Tab_Restoration());
                    return;
                }
                Cincamera.Follow = Spawn_List.UnitList[i];
                Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정
            
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
            }
            if (Cincamera.Follow == null)
            {
                Cincamera.Follow = Spawn_List.UnitList[i];
                Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정
            }
            StartCoroutine(Tab_Change());
        }
    }

    public void targetDead()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        // 타겟을 보는 시네머신은 끄고
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        // 맵을 보는 시네머신은 키고
        i = 0;
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // 순서가 바뀐 뒤 카메라 타겟 설정
    }

    IEnumerator Tab_Restoration()
    { // 메인 캠으로 이동할 함수
        Spawn_List.UnitList[ListIndex].GetComponent<Player_Move>().isMove = false;
        Spawn_List.UnitList[ListIndex].GetComponent<Player_Move>().enabled = false;
        Spawn_List.UnitList[ListIndex].GetComponent<Castle_Spawn>().enabled = false;
        // 리스트 카운트 순서에 있는 타겟의 움직임 스크립트를 끄기
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        // 타겟을 보는 시네머신은 끄고
        yield return new WaitForSeconds(0.1f);
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        // 맵을 보는 시네머신은 키고
        i = 0;
        ListIndex = 0;
        // 현재 순서를 처음으로 되돌리기
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // 순서가 바뀐 뒤 카메라 타겟 설정
    }

    IEnumerator Tab_Change()
    { // 시네머신 캠으로 이동할 함수
        ListIndex = i;
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        // 맵을 보는 시네머신은 끄고
        GetComponent<CinemachineVirtualCamera>().enabled = true;
        // 타겟을 보는 시네머신은 키고
        yield return new WaitForSeconds(0.1f);
        Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
        Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
        Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
        // 현재 순서의 타겟 움직임 스크립트 키기
    }
}
// 알게 된 점 bool 값은 키다운에서 필수였던것이다 // 아니였다
// 코드의 순서는 중요하다
