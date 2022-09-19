using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float range = 2;

    public LayerMask castleMask;
    public List<Transform> CastleList = new List<Transform>();

    public string PlayerTag = "Player";
    public List<GameObject> PlayerList = new List<GameObject>();

    private void Update()
    {
        //Spawn_Limit_Range();
        Spawn_Limit_Target();
    }

    public void Spawn_Limit_Range()
    {
        CastleList.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range + 1, castleMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Transform target = colliders[i].transform;
            CastleList.Add(target);
        }
        if (CastleList.Count >= 1)
        {
            range = range + CastleList.Count;
        }
        else
            range = 2;
    }

    void Spawn_Limit_Target()
    {
        PlayerList.Clear();
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach (GameObject Player in players)
        {
            float distanceTogameObject = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceTogameObject < shortesDistance)
            {
                shortesDistance = distanceTogameObject;
                nearestPlayer = Player;
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
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
