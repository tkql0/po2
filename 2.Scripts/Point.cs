using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // 최대 체력
    public LayerMask playerMask;

    public GameObject panel;

    float range = 8f;

    public string PlayerTag = "Player";
    public List<Transform> PlayerList = new List<Transform>();

    private void Update()
    {
        if (PointHealth < 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        Spawn_Limit_Target();
    }

    void Spawn_Limit_Target()
    {
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
                float dstToTarget = Vector3.Distance(transform.position, players[i].transform.position);
                // 반복중인 오브젝트와의 거리를 계산해 distanceTogameObject에 대입

                if (dstToTarget <= range)
                {
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = true;
                }

                else if (dstToTarget >= range)
                {
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = false;
                }
            }
        }
    }

    public void ReStrat()
    {
        SceneManager.LoadScene(0);
    }


    private void OnDrawGizmosSelected()
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
