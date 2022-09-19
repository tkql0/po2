using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // �ִ� ü��
    public int curHealth;
    // ���� ü��

    float range = 8f;
    
    public string PlayerTag = "Player";
    public List<GameObject> PlayerList = new List<GameObject>();

    private void Start()
    {
        curHealth = PointHealth;
    }

    private void Update()
    {
        Spawn_Limit_Target();
    }

    void Spawn_Limit_Target()
    {
        PlayerList.Clear();
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        float shortesDistance = Mathf.Infinity;
        foreach (GameObject Player in players)
        {
            float distanceTogameObject = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceTogameObject < shortesDistance)
            {
                shortesDistance = distanceTogameObject;
                PlayerList.Add(Player);
            }
        }
        if (PlayerList.Count >= 1)
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                if (shortesDistance <= range)
                {
                    PlayerList[i].GetComponent<Castle_Spawn>().Spawn_Limit = true;
                }
                else
                {
                    PlayerList[i].GetComponent<Castle_Spawn>().Spawn_Limit = false;
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
