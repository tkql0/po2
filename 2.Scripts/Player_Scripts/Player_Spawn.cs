using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    public Transform Player_Group;

    public List<Transform> Player_Unit_List = new List<Transform>();

    public void Unit_Spawn()
    {
        GameObject Unit_Player = Instantiate(Player, transform.position + new Vector3(0, 0, -6f), Player.transform.rotation, Player_Group);
        Player player = Unit_Player.GetComponent<Player>();
        player.Job_Index = Random.Range(0, 3);

        Player_Unit_List.Add(Unit_Player.transform);
    }
}
