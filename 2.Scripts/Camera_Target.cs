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
    // ���� ����

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
            // Ÿ���� ���� �ó׸ӽ��� ����
            MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            // ���� ���� �ó׸ӽ��� Ű��
            i = 0;
            ListIndex = 0;
            // ���� ������ ó������ �ǵ�����
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
            // ������ �ٲ� �� ī�޶� Ÿ�� ����
        }
        KeyDownTab();
    }


    public void KeyDownTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        { // Tab�� ������ �ó׸ӽ� ī�޶� Ÿ���� ����� ���� ����Ʈ ������� ���� ����
            if (Spawn_List.UnitList.Count == 0)
                return;
            if (Cincamera.Follow != null)
            { // Ÿ���� ���� ���� ����
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = false;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = false;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = false;
                // ���� ������ �ٲ�� ���� ��ũ��Ʈ ����
                i++;
                // ��� ������ ���� �� ���� ���� +1
                if (i >= Spawn_List.UnitList.Count)
                { // ���� ������ ������ ������ Ŭ ���
                    StartCoroutine(Tab_Restoration());
                    return;
                }
                Cincamera.Follow = Spawn_List.UnitList[i];
                Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
                // ������ �ٲ� �� ī�޶� Ÿ�� ����
            
                Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
                Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
                Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
            }
            if (Cincamera.Follow == null)
            {
                Cincamera.Follow = Spawn_List.UnitList[i];
                Cincamera.LookAt = Spawn_List.UnitList[i].GetChild(0);
                // ������ �ٲ� �� ī�޶� Ÿ�� ����
            }
            StartCoroutine(Tab_Change());
        }
    }

    public void targetDead()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        // Ÿ���� ���� �ó׸ӽ��� ����
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        // ���� ���� �ó׸ӽ��� Ű��
        i = 0;
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // ������ �ٲ� �� ī�޶� Ÿ�� ����
    }

    IEnumerator Tab_Restoration()
    { // ���� ķ���� �̵��� �Լ�
        Spawn_List.UnitList[ListIndex].GetComponent<Player_Move>().isMove = false;
        Spawn_List.UnitList[ListIndex].GetComponent<Player_Move>().enabled = false;
        Spawn_List.UnitList[ListIndex].GetComponent<Castle_Spawn>().enabled = false;
        // ����Ʈ ī��Ʈ ������ �ִ� Ÿ���� ������ ��ũ��Ʈ�� ����
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        // Ÿ���� ���� �ó׸ӽ��� ����
        yield return new WaitForSeconds(0.1f);
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        // ���� ���� �ó׸ӽ��� Ű��
        i = 0;
        ListIndex = 0;
        // ���� ������ ó������ �ǵ�����
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // ������ �ٲ� �� ī�޶� Ÿ�� ����
    }

    IEnumerator Tab_Change()
    { // �ó׸ӽ� ķ���� �̵��� �Լ�
        ListIndex = i;
        MainCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        // ���� ���� �ó׸ӽ��� ����
        GetComponent<CinemachineVirtualCamera>().enabled = true;
        // Ÿ���� ���� �ó׸ӽ��� Ű��
        yield return new WaitForSeconds(0.1f);
        Spawn_List.UnitList[i].GetComponent<Player_Move>().isMove = true;
        Spawn_List.UnitList[i].GetComponent<Player_Move>().enabled = true;
        Spawn_List.UnitList[i].GetComponent<Castle_Spawn>().enabled = true;
        // ���� ������ Ÿ�� ������ ��ũ��Ʈ Ű��
    }
}
// �˰� �� �� bool ���� Ű�ٿ�� �ʼ��������̴� // �ƴϿ���
// �ڵ��� ������ �߿��ϴ�
