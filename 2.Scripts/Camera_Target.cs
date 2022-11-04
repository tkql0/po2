using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Target : MonoBehaviour
{
    CinemachineVirtualCamera Cincamera;
    public CinemachineVirtualCamera Main_Cincamera;

    Player_Spawn Player_Spawn_List;

    public GameObject MainCam;

    public int now_Index = 0;
    // 현재 순서
    public int Past_Index = 0;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
        Main_Cincamera = MainCam.GetComponent<CinemachineVirtualCamera>();
        Player_Spawn_List = GameManager.Instance.point.GetComponent<Player_Spawn>();
    }

    private void Update()
    {
        Target_None();
        KeyDownTab();
    }

    void Target_None()
    {
        if (now_Index >= Player_Spawn_List.Player_Unit_List.Count)
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
            if (Player_Spawn_List.Player_Unit_List.Count == 0)
                return;
            if (Cincamera.Follow != null)
            { // 타겟이 있을 때만 실행
                Target_Switch_Off(now_Index);
                // 현재 순서가 바뀌기 전에 스크립트 끄기
                now_Index++;
                // 모든 순서가 끝난 후 현재 순서 +1
                if (now_Index >= Player_Spawn_List.Player_Unit_List.Count)
                { // 현재 순서가 스폰된 수보다 클 경우
                    StartCoroutine(Tab_Restoration());
                    return;
                }
                Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정

                Target_Switch_On(now_Index);
            }
            if (Cincamera.Follow == null)
            {
                Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // 순서가 바뀐 뒤 카메라 타겟 설정
            }
            StartCoroutine(Tab_Change());
        }
    }

    IEnumerator Tab_Restoration()
    { // 메인 캠으로 이동할 함수
        Target_Switch_Off(Past_Index);
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
        if (Player_Spawn_List.Player_Unit_List.Count > now_Index)
        {
            Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
            Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
            // 순서가 바뀐 뒤 카메라 타겟 설정

            Target_Switch_On(now_Index);
        }

        else if (Player_Spawn_List.Player_Unit_List.Count == now_Index && now_Index != 0)
        {
            now_Index--;
            Past_Index--;
            Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
            Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
            // 순서가 바뀐 뒤 카메라 타겟 설정

            Target_Switch_On(now_Index);
        }

        else
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
    }

    IEnumerator Tab_Change()
    { // 시네머신 캠으로 이동할 함수
        Past_Index = now_Index;
        Main_Cincamera.enabled = false;
        // 맵을 보는 시네머신은 끄고
        Cincamera.enabled = true;
        // 타겟을 보는 시네머신은 키고
        yield return new WaitForSeconds(0.1f);
        Target_Switch_On(now_Index);
        // 현재 순서의 타겟 움직임 스크립트 키기
    }

    void Target_Switch_On(int i)
    {
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Player_Move>().isMove = true;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Player_Move>().enabled = true;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Castle_Spawn>().enabled = true;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Bow_Shoot>().enabled = true;
    }

    void Target_Switch_Off(int i)
    {
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Player_Move>().isMove = false;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Player_Move>().enabled = false;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Castle_Spawn>().enabled = false;
        Player_Spawn_List.Player_Unit_List[i].GetComponent<Bow_Shoot>().enabled = false;
    }
}
