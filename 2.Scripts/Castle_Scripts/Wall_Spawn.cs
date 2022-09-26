using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Spawn : MonoBehaviour
{
    // Ÿ�� �±� Castle �߰�
    // ���� ���� �ȿ� ������ ���̿� ����
    // �巡�� Ŭ���� �����ؼ� �� ������ ���̸�ŭ �þ�� �ϱ�

    public string CastleTag = "Castle";

    public Transform spawn_target;

    public float wallrange = 3f;

    public GameObject Wall;
    // ���� �� ���̿� ����
    // �ٸ� ������ ������ �Ÿ��� ����ϰ� �Ÿ��� 1/2��ŭ�� �Ÿ��� ����
    // ������ 2���� �ְ� �� �ȿ� �ִٸ� �ִ� ����ŭ �ݺ� // start�Լ��� �ؾ߰ڴ� // ������ ���ÿ� �ֺ�������Ʈ�� Ž���ϰ� �� ����

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
                    // �� ������ �����ǰ� �ؼ� ���� �ı��ɽ� ���� �ı�
                    // �ִϸ��̼��� �ִ´ٸ� �÷��̾ �ɾ�� ����� ������ �������� �ϱ�
                }
            }
        }
    }
}
