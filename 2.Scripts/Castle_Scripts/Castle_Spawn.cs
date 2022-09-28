using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Castle_Spawn : MonoBehaviour
{
    public GameObject Unit_Castle;
    // ������ ������Ʈ
    public GameObject Castle_Withdrawal;

    public Player player;

    public string CastleTag = "Castle";

    public bool Point_Spawn_Limit = false;
    public bool Castle_Spawn_Limit = false;

    private void Update()
    {
        Castle_spown();
    }

    void Castle_spown()
    {
        // �׺�޽������� ���� ��ġ�Ҽ��ִ� ������ ���� ���������� �ֺ��� ī��Ʈ �ڽ� ī��Ʈ ������ �ø���
        // ��ü ������ 2~3 ���� �������� ��ġ�Ҽ��ְ�
        // �̷��� ��� ������ ���ѷ��� ������ �ʰ���
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

        if (Input.GetKeyDown(KeyCode.LeftAlt) && !player.isDead)
        {
            if (Point_Spawn_Limit == true || Castle_Spawn_Limit == true)
            {
                GameObject Castle_range = Instantiate(Castle_Withdrawal, transform.position, transform.rotation, this.transform);
                Destroy(Castle_range, 1.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt) && !player.isDead)
        {
            if (Point_Spawn_Limit == false && Castle_Spawn_Limit == false)
            {
                Spawn_Unit();
                Destroy(gameObject);
            }
        }
    }

    public void Spawn_Unit()
    {
        GameObject gameObject = Instantiate(Unit_Castle, transform.position + new Vector3(0, 2f, 0), transform.rotation, GameManager.Instance.Castle_Group);
        Castle castle = gameObject.GetComponent<Castle>();
        castle.Spawn_Limit_Range();
        GameManager.Instance.Castle_List.Add(gameObject.transform);
        GameManager.Instance.player_spawn.UnitList.Remove(transform);
    }
}
