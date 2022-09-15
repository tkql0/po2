using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Targerting : MonoBehaviour
{
    public float range = 6f;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�

    public float Cool_Time = 4f;
    public float Cool_Time_Down = 0f;
    // ���� ��Ÿ��
    public GameObject Danger;
    public Transform target;

    public string EnemyTag = "Enemy";

    private void Start()
    {
        InvokeRepeating("Search", 0f, 0.5f);
    }

    private void Update()
    {

    }

    void Search()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(EnemyTag);
        // ������ ������Ʈ �� Enemy �±װ� ���� ������Ʈ ����
        float shortesDistance = Mathf.Infinity;
        // shortesDistance �������� �ʱ�ȭ
        GameObject nearestEnemy = null;
        // ���� ����

        foreach (GameObject Enemy in enemys)
        { // �����Ǿ��ִ� ���͸�ŭ �ݺ�
            float distanceToPlayer = Vector3.Distance(transform.position, Enemy.transform.position);
            // �ڽŰ� ���Ϳ��� �Ÿ��� ����
            if (distanceToPlayer < shortesDistance)
            { // distanceToPlayer�� shortesDistance���� �۴ٸ�
                shortesDistance = distanceToPlayer;
                // shortesDistance�� distanceToPlayer ����
                nearestEnemy = Enemy;
                // ���� ����
            }
        }
        if (nearestEnemy != null && shortesDistance <= range)
        {
            target = nearestEnemy.transform;
            Danger.SetActive(true);
        }

        else
        {
            target = null;
            Danger.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
