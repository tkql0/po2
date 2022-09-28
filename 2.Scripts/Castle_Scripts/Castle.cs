using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float range = 2;

    public GameObject Range;

    public LayerMask playerMask, castleMask;
    public List<Transform> CastleList = new List<Transform>();
    public List<Transform> PlayerList = new List<Transform>();

    private void Update()
    {
        Spawn_Limit_Target();
    }

    public void Spawn_Limit_Range()
    {
        CastleList.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range + 2, castleMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Transform target = colliders[i].transform;
            CastleList.Add(target);
        }
        if (CastleList.Count >= 1)
        {
            range = range + CastleList.Count + 1;
        }
    }

    void Spawn_Limit_Target()
    {
        PlayerList.Clear();
        Collider[] players = Physics.OverlapSphere(transform.position, range, playerMask);

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

                if (dstToTarget <= range)
                {
                    PlayerList[i].GetComponent<Castle_Spawn>().Castle_Spawn_Limit = true;
                    Range.transform.localScale = new Vector3(range + 2, range + 2, range + 2);
                    Range.SetActive(true);
                }

                else if(dstToTarget >= range)
                {
                    PlayerList[i].GetComponent<Castle_Spawn>().Castle_Spawn_Limit = false;
                    Range.SetActive(false);
                    PlayerList.RemoveAt(i);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
