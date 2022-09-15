using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Targerting : MonoBehaviour
{
    public float range = 6f;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리

    public float Cool_Time = 4f;
    public float Cool_Time_Down = 0f;
    // 공격 쿨타임
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
        // 생성된 오브젝트 중 Enemy 태그가 붙은 오브젝트 저장
        float shortesDistance = Mathf.Infinity;
        // shortesDistance 무한으로 초기화
        GameObject nearestEnemy = null;
        // 몬스터 없음

        foreach (GameObject Enemy in enemys)
        { // 생성되어있는 몬스터만큼 반복
            float distanceToPlayer = Vector3.Distance(transform.position, Enemy.transform.position);
            // 자신과 몬스터와의 거리를 저장
            if (distanceToPlayer < shortesDistance)
            { // distanceToPlayer가 shortesDistance보다 작다면
                shortesDistance = distanceToPlayer;
                // shortesDistance에 distanceToPlayer 대입
                nearestEnemy = Enemy;
                // 몬스터 있음
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
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
