using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField]
    float range;

    public GameObject Range;

    public LayerMask playerMask, castleMask;
    List<Transform> CastleList = new List<Transform>();
    List<Transform> PlayerList = new List<Transform>();

    private void Update()
    {
        Spawn_Limit_Target();
    }

    public void Spawn_Limit_Range()
    { // 범위 안에 있는 Castle의 수만큼 생성 제한범위 증가
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
    { // 범위 내에서 Player의 Castle 생성 제한
        PlayerList.Clear();
        Collider[] players = Physics.OverlapSphere(transform.position, range, playerMask);

        for (int i = 0; i < players.Length; i++)
        { // 범위안에 있는 플레이어를 받아와서 저장
            Transform target = players[i].transform;
            // i번째 타겟을 대입
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
}
