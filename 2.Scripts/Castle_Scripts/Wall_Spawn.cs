using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Spawn : MonoBehaviour
{
    string CastleTag = "Castle";

    Transform spawn_target;

    [SerializeField]
    float wallrange = 3f;

    public GameObject Wall;

    private void Start()
    {
        spawn_target = this.transform;
        GameObject[] castles = GameObject.FindGameObjectsWithTag(CastleTag);
        // ���� �±��� ������Ʈ�� ����
        foreach (GameObject Castle in castles)
        { // ����� ������Ʈ ����ŭ �ݺ�
            Vector3 dir = Castle.transform.position - transform.position;

            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir.normalized);
                float distanceTogameObject = Vector3.Distance(transform.position, Castle.transform.position);
                // �ݺ����� ������Ʈ���� �Ÿ��� ����� distanceTogameObject�� ����
                if (distanceTogameObject <= wallrange)
                { // distanceTogameObject�� ������ ���� wallrange���� �۴ٸ� wallrange�� 1/2 �Ÿ��� wallrange��ŭ�� ���̸� ���� ������Ʈ�� rot�� �������� ����
                    GameObject wall = Instantiate(Wall, new Vector3((transform.position.x + Castle.transform.position.x) / 2, transform.position.y, (transform.position.z + Castle.transform.position.z) / 2), rot, spawn_target);
                    wall.transform.localScale += new Vector3(0, 0, distanceTogameObject / 2);
                }
            }
        }
    }
}
