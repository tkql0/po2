using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Castle_Spawn : MonoBehaviour
{
    public GameObject Unit_Castle;
    // 생성될 오브젝트
    public GameObject Castle_Withdrawal;

    public Player player;
    public float range = 1.5f;

    public int count = 1;

    public int a;

    public string CastleTag = "Castle";

    private RaycastHit hitInfo;

    private void Awake()
    {
        a = player.count;
    }

    private void Update()
    {
            GameObject[] castles = GameObject.FindGameObjectsWithTag(CastleTag);
            float shortesDistance = Mathf.Infinity;

            foreach (GameObject Castle in castles)
            {
                float distanceTogameObject = Vector3.Distance(transform.position, Castle.transform.position);
                if (distanceTogameObject < shortesDistance)
                {
                    shortesDistance = distanceTogameObject;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt) && count == 1)
            {
                if (shortesDistance <= range)
                {
                    GameObject Castle_range = Instantiate(Castle_Withdrawal, transform.position, transform.rotation, this.transform);
                    Destroy(Castle_range, 1.5f);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt) && count == 1)
            {
                if (shortesDistance >= range)
                {
                    count = count - 1;
                    gameObject.SetActive(false);
                    GameManager.Instance.player_spawn.UnitList.RemoveAt(a);
                    Spawn_Unit();
                }
        }
    }

    public void Spawn_Unit()
    {
        GameObject gameObject = Instantiate(Unit_Castle, transform.position, transform.rotation, GameManager.Instance.Castle_Group);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
