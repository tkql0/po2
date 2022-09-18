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
    public float range;

    public int count = 1;

    public string CastleTag = "Castle";

    private RaycastHit hitInfo;
    public List<GameObject> CastleList = new List<GameObject>();

    private void Update()
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
                CastleList.Add(Castle);
            }
        }

        for(int i = CastleList.Count - 1; i < 0; i--)
        {
            if (shortesDistance <= range)
            {
                CastleList[i].GetComponent<Castle_Spawn>().enabled = false;
                // ����Ƽ ���� // �۰� �����ϱ�
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && count == 1 && !player.isDead)
        {
            if (shortesDistance <= range)
            {
                GameObject Castle_range = Instantiate(Castle_Withdrawal, transform.position, transform.rotation, this.transform);
                Destroy(Castle_range, 1.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt) && count == 1 && !player.isDead)
        {
            if (shortesDistance > range)
            {
                count = count - 1;
                gameObject.SetActive(false);
                Spawn_Unit();
                range = range + 1;
            }
        }
    }

    public void Spawn_Unit()
    {
        GameObject gameObject = Instantiate(Unit_Castle, transform.position + new Vector3(0, 2f, 0), transform.rotation, GameManager.Instance.Castle_Group);
        GameManager.Instance.Castle_List.Add(gameObject.transform);
    }
}
