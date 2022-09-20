using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    // 캐릭터가 죽으면 UnitList를 Remove 시키기
    [SerializeField]
    private GameObject Player;
    public int count = 0;

    public Transform Player_Group;

    public List<Transform> UnitList = new List<Transform>();

    public void Unit_Spawn()
    {
        GameObject Unit_Player = Instantiate(Player, transform.position + new Vector3(0, 0, -6f), Player.transform.rotation, Player_Group);
        Player player = Unit_Player.GetComponent<Player>();
        player.count = count;
        count++;
        player.Job_Index = Random.Range(0, 3);
        // 1은 활 2은 방패 0은 무직

        UnitList.Add(Unit_Player.transform);
    }
}
