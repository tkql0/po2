using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Targerting : MonoBehaviour
{
    public float range;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�

    public float Cool_Time = 4f;
    public float Cool_Time_Down = 0f;
    // ���� ��Ÿ��
    public int Castle_damage;

    public GameObject Danger;
    public Transform target;

    public GameObject castle_Shoot;

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
            StartCoroutine(Shoot());
        }

        else
        {
            target = null;
            Danger.SetActive(false);
        }
    }

    IEnumerator Shoot()
    {
        GameObject Attack = Instantiate(castle_Shoot, target.GetChild(0).position, target.rotation);
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.curHealth = enemy.curHealth - Castle_damage;
        Destroy(Attack, 2f);
        yield return new WaitForSeconds(Cool_Time);
    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
