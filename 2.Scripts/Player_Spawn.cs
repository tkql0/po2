using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    // ĳ���Ͱ� ������ UnitList�� Remove ��Ű��
    [SerializeField]
    private GameObject Player;
    public int count = 0;

    public Transform Player_Group;

    public List<Transform> UnitList = new List<Transform>();

    public void Unit_Spawn()
    {
        GameObject Unit_Player = Instantiate(Player, transform.position + new Vector3(0, 0, -3f), Player.transform.rotation, Player_Group);
        //Unit_Choice unit = Unit_Player.GetComponent<Unit_Choice>();
        Player player = Unit_Player.GetComponent<Player>();
        player.count = count;
        count++;
        //player.job_count = Random.Range(0, 3);
        // 1�� Ȱ 2�� ���� 0�� ����

        UnitList.Add(Unit_Player.transform);
    }
}
