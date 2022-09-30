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
    // ���� ����
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
            // Ÿ���� ���� �ó׸ӽ��� ����
            Main_Cincamera.enabled = true;
            // ���� ���� �ó׸ӽ��� Ű��
            now_Index = 0;
            Past_Index = 0;
            // ���� ������ ó������ �ǵ�����
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
            // ������ �ٲ� �� ī�޶� Ÿ�� ����
        }
    }

    public void KeyDownTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetMouseButton(0))
        { // Tab�� ������ �ó׸ӽ� ī�޶� Ÿ���� ����� ���� ����Ʈ ������� ���� ����
            if (Player_Spawn_List.Player_Unit_List.Count == 0)
                return;
            if (Cincamera.Follow != null)
            { // Ÿ���� ���� ���� ����
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = false;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = false;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = false;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = false;
                // ���� ������ �ٲ�� ���� ��ũ��Ʈ ����
                now_Index++;
                // ��� ������ ���� �� ���� ���� +1
                if (now_Index >= Player_Spawn_List.Player_Unit_List.Count)
                { // ���� ������ ������ ������ Ŭ ���
                    StartCoroutine(Tab_Restoration());
                    return;
                }
                Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // ������ �ٲ� �� ī�޶� Ÿ�� ����

                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
                Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
            }
            if (Cincamera.Follow == null)
            {
                Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
                Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
                // ������ �ٲ� �� ī�޶� Ÿ�� ����
            }
            StartCoroutine(Tab_Change());
        }
    }

    IEnumerator Tab_Restoration()
    { // ���� ķ���� �̵��� �Լ�
        Player_Spawn_List.Player_Unit_List[Past_Index].GetComponent<Player_Move>().isMove = false;
        Player_Spawn_List.Player_Unit_List[Past_Index].GetComponent<Player_Move>().enabled = false;
        Player_Spawn_List.Player_Unit_List[Past_Index].GetComponent<Castle_Spawn>().enabled = false;
        Player_Spawn_List.Player_Unit_List[Past_Index].GetComponent<Bow_Shoot>().enabled = false;
        // ����Ʈ ī��Ʈ ������ �ִ� Ÿ���� ������ ��ũ��Ʈ�� ����
        Cincamera.enabled = false;
        // Ÿ���� ���� �ó׸ӽ��� ����
        yield return new WaitForSeconds(0.1f);
        Main_Cincamera.enabled = true;
        // ���� ���� �ó׸ӽ��� Ű��
        now_Index = 0;
        Past_Index = 0;
        // ���� ������ ó������ �ǵ�����
        Cincamera.Follow = null;
        Cincamera.LookAt = null;
        // ������ �ٲ� �� ī�޶� Ÿ�� ����
    }

    public void Player_Dead()
    {
        if (Player_Spawn_List.Player_Unit_List.Count > now_Index)
        {
            Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
            Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
            // ������ �ٲ� �� ī�޶� Ÿ�� ����

            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
        }

        else if (Player_Spawn_List.Player_Unit_List.Count == now_Index && now_Index != 0)
        {
            now_Index--;
            Past_Index--;
            Cincamera.Follow = Player_Spawn_List.Player_Unit_List[now_Index];
            Cincamera.LookAt = Player_Spawn_List.Player_Unit_List[now_Index].GetChild(0);
            // ������ �ٲ� �� ī�޶� Ÿ�� ����

            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
            Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
        }

        else
        {
            Cincamera.enabled = false;
            // Ÿ���� ���� �ó׸ӽ��� ����
            Main_Cincamera.enabled = true;
            // ���� ���� �ó׸ӽ��� Ű��
            now_Index = 0;
            Past_Index = 0;
            // ���� ������ ó������ �ǵ�����
            Cincamera.Follow = null;
            Cincamera.LookAt = null;
        }
    }

    IEnumerator Tab_Change()
    { // �ó׸ӽ� ķ���� �̵��� �Լ�
        Past_Index = now_Index;
        Main_Cincamera.enabled = false;
        // ���� ���� �ó׸ӽ��� ����
        Cincamera.enabled = true;
        // Ÿ���� ���� �ó׸ӽ��� Ű��
        yield return new WaitForSeconds(0.1f);
        Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().isMove = true;
        Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Player_Move>().enabled = true;
        Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Castle_Spawn>().enabled = true;
        Player_Spawn_List.Player_Unit_List[now_Index].GetComponent<Bow_Shoot>().enabled = true;
        // ���� ������ Ÿ�� ������ ��ũ��Ʈ Ű��
    }
}
