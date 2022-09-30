using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Spawn : MonoBehaviour
{
    public GameObject Unit_Castle;
    // 생성될 오브젝트
    public GameObject Castle_Limit;

    Player player;

    public bool Point_Spawn_Limit = false;
    public bool Castle_Spawn_Limit = false;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Castle_spown();
    }

    void Castle_spown()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !player.isDead)
        {
            if (Point_Spawn_Limit == true || Castle_Spawn_Limit == true)
            {
                GameObject Castle_range = Instantiate(Castle_Limit, transform.position, transform.rotation, this.transform);
                Destroy(Castle_range, 1.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !player.isDead)
        {
            if (Point_Spawn_Limit == false && Castle_Spawn_Limit == false)
            {
                Spawn_Castle_Unit();
                Destroy(gameObject);
            }
        }
    }

    void Spawn_Castle_Unit()
    {
        GameObject gameObject = Instantiate(Unit_Castle, transform.position + new Vector3(0, 2f, 0), transform.rotation, GameManager.Instance.Castle_Group);
        Castle castle = gameObject.GetComponent<Castle>();
        castle.Spawn_Limit_Range();
        GameManager.Instance.Castle_List.Add(gameObject.transform);
        GameManager.Instance.player_spawn.Player_Unit_List.Remove(transform);
    }
}
