using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField]
    float Castle_Limit_Range;

    public GameObject Range;

    public LayerMask playerMask, castleMask;
    List<Transform> CastleList = new List<Transform>();
    List<Transform> PlayerList = new List<Transform>();

    private void Update()
    {
        Spawn_Limit_Target();
    }

    public void Spawn_Limit_Range()
    { // ���� �ȿ� �ִ� Castle�� ����ŭ ���� ���ѹ��� ����
        CastleList.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, Castle_Limit_Range + 2, castleMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Transform target = colliders[i].transform;
            CastleList.Add(target);
        }

        if (CastleList.Count >= 1)
            Castle_Limit_Range = Castle_Limit_Range + CastleList.Count + 1;
    }

    void Spawn_Limit_Target()
    { // ���� ������ Player�� Castle ���� ����
        PlayerList.Clear();
        Collider[] players = Physics.OverlapSphere(transform.position, Castle_Limit_Range, playerMask);

        for (int i = 0; i < players.Length; i++)
        { // �����ȿ� �ִ� �÷��̾ �޾ƿͼ� ����
            Transform target = players[i].transform;
            // i��° Ÿ���� ����
            PlayerList.Add(target);
        }

        if (PlayerList.Count != 0)
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                float dstToTarget = Vector3.Distance(transform.position, PlayerList[i].transform.position);

                if (dstToTarget <= Castle_Limit_Range)
                { // Player�� Castle�� �Ÿ��� ���ѹ������� �����ٸ�
                    PlayerList[i].GetComponent<Castle_Spawn>().Castle_Spawn_Limit = true;
                    Range.transform.localScale = new Vector3(Castle_Limit_Range + 2, Castle_Limit_Range + 2, Castle_Limit_Range + 2);
                    Range.SetActive(true);
                }

                else if(dstToTarget >= Castle_Limit_Range)
                {
                    PlayerList[i].GetComponent<Castle_Spawn>().Castle_Spawn_Limit = false;
                    Range.SetActive(false);
                    PlayerList.RemoveAt(i);
                }
            }
        }
    }
}