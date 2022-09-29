using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Target : MonoBehaviour
{
    CinemachineVirtualCamera Cincamera;
    CinemachineVirtualCamera Main_Cincamera;

    public Player_Spawn Spawn_List;

    public GameObject MainCam;

    public int now_Index = 0;
    // 현재 순서
    public int Past_Index = 0;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
        Main_Cincamera = MainCam.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        Target_None();
        KeyDownTab();
    }

    void Target_None()
    {
        if (now_Index >= Spawn_List.Player_Unit_List.Count)
        {
            Cincamera.enabled = false;
            // 타겟을 보는 시네머신은 끄고
            Main_Cincamera.enabled = true;
            // 맵을 보는 시네머신은 키고
            now_Index = 0;
            Past_Index = 0;
            // 현재 순서를 처음으로 되돌리기
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
            // 순서가 바뀐 뒤 카메라 타겟 설정
        }
    }

    public void KeyDownTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetMouseButton(0))
        { // Tab을 누르면 시네머신 카메라가 타겟을 저장된 스폰 리스트 순서대로 시점 변경
            if (Spawn_List.Player_Unit_List.Count == 0)
                return;
            if (Cincamera.Follow != null)
            { // 타겟이 있을 때만 실행
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = false;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = false;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = false;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = false;
                // 현재 순서가 바뀌기 전에 스크립트 끄기
                now_Index++;
                // 모든 순서가 끝난 후 현재 순서 +1
                if (now_Index >= Spawn_List.Player_Unit_List.Count)
                { // 현재 순서가 스폰된 수보다 클 경우
                    StartCoroutine(Tab_Restoration());
                    return;
                }
                Cincamera.Follow = Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정
            
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
                Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
            }
            if (Cincamera.Follow == null)
            {
                Cincamera.Follow = Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정
            }
            StartCoroutine(Tab_Change());
        }
    }

    IEnumerator Tab_Restoration()
    { // 메인 캠으로 이동할 함수
        Spawn_List.Player_Unit_List[Past_Index].GetComponent<Player_Move>().isMove = false;
        Spawn_List.Player_Unit_List[Past_Index].GetComponent<Player_Move>().enabled = false;
        Spawn_List.Player_Unit_List[Past_Index].GetComponent<Castle_Spawn>().enabled = false;
        Spawn_List.Player_Unit_List[Past_Index].GetComponent<Bow_Shoot>().enabled = false;
        // 리스트 카운트 순서에 있는 타겟의 움직임 스크립트를 끄기
        Cincamera.enabled = false;
        // 타겟을 보는 시네머신은 끄고
        yield return new WaitForSeconds(0.1f);
        Main_Cincamera.enabled = true;
        // 맵을 보는 시네머신은 키고
        now_Index = 0;
        Past_Index = 0;
        // 현재 순서를 처음으로 되돌리기
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // 순서가 바뀐 뒤 카메라 타겟 설정
    }

    public void Player_Dead()
    {
        if (Spawn_List.Player_Unit_List[now_Index] == null)
        {
            Cincamera.enabled = false;
            // 타겟을 보는 시네머신은 끄고
            Main_Cincamera.enabled = true;
            // 맵을 보는 시네머신은 키고
            now_Index = 0;
            Past_Index = 0;
            // 현재 순서를 처음으로 되돌리기
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
        }

        if (Spawn_List.Player_Unit_List[now_Index] != null)
        {
            Cincamera.Follow = Spawn_List.Player_Unit_List[now_Index];
            Cincamera.LookAt = Spawn_List.Player_Unit_List[now_Index].GetChild(0);
            // 순서가 바뀐 뒤 카메라 타겟 설정

            Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
            Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
            Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
            Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
        }
    }

    IEnumerator Tab_Change()
    { // 시네머신 캠으로 이동할 함수
        Past_Index = now_Index;
        Main_Cincamera.enabled = false;
        // 맵을 보는 시네머신은 끄고
        Cincamera.enabled = true;
        // 타겟을 보는 시네머신은 키고
        yield return new WaitForSeconds(0.1f);
        Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
        Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
        Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
        Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
        // 현재 순서의 타겟 움직임 스크립트 키기
    }
}
// 알게 된 점 bool 값은 키다운에서 필수였던것이다 // 아니였다
// 코드의 순서는 중요하다
