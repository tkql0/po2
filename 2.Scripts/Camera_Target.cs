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
    // ���� ����
    private int ListCount = 0;
    // ���� ������ ������ ����Ʈ ī��Ʈ ����

    //bool isMove;

    private void Start()
    {
        Cincamera = GetComponent<CinemachineVirtualCamera>();
        //FreeLook = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        { // Tab�� ������ �ó׸ӽ� ī�޶� Ÿ���� ����� ���� ����Ʈ ������� ���� ����
            // �������� �����°� �����ϱ� ���� bool�� Ű�� 
            StartCoroutine(Tab_Restoration());
            Spawn_List.UnitList[ListCount].GetComponent<Player_Move>().isMove = false;
            Cincamera.Follow = Spawn_List.UnitList[i];
            Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
            // Tab�� �������� ��������Ʈ�� ����� Ÿ���� �ó׸ӽ� ī�޶� ����
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
            // ���� ���� �ó׸ӽ��� ����
            GetComponent<CinemachineVirtualCamera>().enabled = true;
            // Ÿ���� ���� �ó׸ӽ��� Ű��
            StartCoroutine(Tab_Change());
        }
    }

    IEnumerator Tab_Change()
    {
        Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
        if (ListCount != i)
        // ����Ʈ ī��Ʈ�� ���� ������ �ٸ���
        {
            Spawn_List.UnitList[ListCount].GetComponent<Player_Move>().enabled = false;
            Spawn_List.UnitList[ListCount].GetComponent<Castle_Spawn>().enabled = false;
        }
        ListCount = i;
        // ����Ʈ ī��Ʈ�� ���� ���� ����
        Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
        Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
        // ���� ������ Ÿ�� ������ ��ũ��Ʈ Ű��
        i++;
        // ��� ������ ���� �� ���� ���� +1
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Tab_Restoration()
    { 
        if (i >= Spawn_List.UnitList.Count)
        { // ���� ������ ������ ������ Ŭ ���

            if(Spawn_List.UnitList.Count > ListCount)
            {
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = false;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = false;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = false;
            }
            
            // ����Ʈ ī��Ʈ ������ �ִ� Ÿ���� ������ ��ũ��Ʈ�� ����
            GetComponent<CinemachineVirtualCamera>().enabled = false;
            // Ÿ���� ���� �ó׸ӽ��� ����
            yield return new WaitForSeconds(0.5f);
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            // ���� ���� �ó׸ӽ��� Ű��
            i = 0;
            // ���� ������ ó������ �ǵ����� 
        }
        //yield return new WaitForSeconds(0.5f);
    }
}
// �˰� �� �� bool ���� Ű�ٿ�� �ʼ��������̴�
// �ڵ��� ������ �߿��ϴ�
