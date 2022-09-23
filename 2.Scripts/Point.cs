using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // 최대 체력
    public LayerMask targetMask;

    public GameObject panel;

    float range = 8f;

    public string PlayerTag = "Player";
    public List<GameObject> PlayerList = new List<GameObject>();

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
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        foreach (GameObject Player in players)
        {
            Vector3 dir = Player.transform.position - transform.position;
            if (dir != Vector3.zero && players.Length > 0)
            {
                float distanceTogameObject = Vector3.Distance(transform.position, Player.transform.position);
                // 반복중인 오브젝트와의 거리를 계산해 distanceTogameObject에 대입
                if (distanceTogameObject <= range)
                {
                    Player.GetComponent<Castle_Spawn>().Spawn_Limit = true;
                }
                else
                {
                    Player.GetComponent<Castle_Spawn>().Spawn_Limit = false;
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
