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

    private int i = 0;
    // 현재 순서
    private int ListCount = 0;
    // 현재 순서를 저장한 리스트 카운트 변수

    //bool isMove;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
        //FreeLook = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        { // Tab을 누르면 시네머신 카메라가 타겟을 저장된 스폰 리스트 순서대로 시점 변경
            // 연속으로 눌리는걸 방지하기 위해 bool을 키기 
            StartCoroutine(Tab_Restoration());
            Spawn_List.UnitList[ListCount].GetComponent<Player_Move>().isMove = false;
            Cincamera.Follow = Spawn_List.UnitList[i];
            Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
            // Tab을 눌렀을때 스폰리스트의 저장된 타겟을 시네머신 카메라에 대입
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
            // 맵을 보는 시네머신은 끄고
            GetComponent<CinemachineVirtualCamera>().enabled = true;
            // 타겟을 보는 시네머신은 키고
            StartCoroutine(Tab_Change());
        }
    }

    IEnumerator Tab_Change()
    {
        Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
        if (ListCount != i)
        // 리스트 카운트와 현재 순서가 다르면
        {
            Spawn_List.UnitList[ListCount].GetComponent<Player_Move>().enabled = false;
            Spawn_List.UnitList[ListCount].GetComponent<Castle_Spawn>().enabled = false;
        }
        ListCount = i;
        // 리스트 카운트에 현재 순서 대입
        Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
        Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
        // 현재 순서의 타겟 움직임 스크립트 키기
        i++;
        // 모든 순서가 끝난 후 현재 순서 +1
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Tab_Restoration()
    { 
        if (i >= Spawn_List.UnitList.Count)
        { // 현재 순서가 스폰된 수보다 클 경우

            if(Spawn_List.UnitList.Count > ListCount)
            {
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = false;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = false;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = false;
            }
            
            // 리스트 카운트 순서에 있는 타겟의 움직임 스크립트를 끄기
            GetComponent<CinemachineVirtualCamera>().enabled = false;
            // 타겟을 보는 시네머신은 끄고
            yield return new WaitForSeconds(0.5f);
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            // 맵을 보는 시네머신은 키고
            i = 0;
            // 현재 순서를 처음으로 되돌리기 
        }
        //yield return new WaitForSeconds(0.5f);
    }
}
// 알게 된 점 bool 값은 키다운에서 필수였던것이다
// 코드의 순서는 중요하다
