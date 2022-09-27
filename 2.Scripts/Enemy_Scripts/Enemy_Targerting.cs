using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Targerting : MonoBehaviour
{
    [SerializeField]
    float Enemy_Search_Range;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리
    public NavMeshAgent Enemy_Nav;

    Transform Target_Position;
    // 네비메쉬의 목표오브젝트
    Transform SaveTarget_Position;
    // 목표를 잃었을때 목표오브젝트
    Transform targeting;

    Animator Enemy_anim;

    public LayerMask targetMask, obstacleMask;
    // 타겟과 장애물
    float shortDis;

    List<Transform> target_search = new List<Transform>();
    // 찾은 타겟을 저장할 리스트

    private void Awake()
    {
        Enemy_anim = GetComponentInChildren<Animator>();
        Enemy_Nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Target_Position = GameManager.Instance.Enemy_Target;
        SaveTarget_Position = Target_Position;
        StartCoroutine(InstantiateEnemy());
    }

    IEnumerator InstantiateEnemy()
    { // Enemy 생성 애니메이션이 끝난 후 이동
        Enemy_Nav.speed = 0f;
        yield return new WaitForSeconds(5f);
        Enemy_Nav.speed = 3f;
    }

    private void Update()
    {
        Enemy_Move();
        Enemy_Search();
    }

    void Enemy_Move()
    {
        if (Enemy_Nav.enabled)
        { // 오브젝트가 움직이는 중일때
            if (Target_Position == null)
                Target_Position = SaveTarget_Position;

            Enemy_Nav.SetDestination(Target_Position.transform.position);
            Enemy_anim.SetBool("isWalk", true);

            if (Vector3.Distance(transform.position, SaveTarget_Position.transform.position) <= 3f)
                // 타겟과의 거리가 3만큼 가까워지면 삭제
                DownPoint();
        }
    }

    void Enemy_Search()
    {
        target_search.Clear();
        // 저장된 타겟을 초기화
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Enemy_Search_Range, targetMask);
        // 범위안에 들어온 타겟레이어를 저장

        for (int i = 0; i < inTarget.Length; i++)
        {
            Transform target = inTarget[i].transform;
            // i번째 타겟을 대입
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // 타겟과 자신과의 방향을 저장
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
            // 타겟과 자신과의 거리를 저장
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
             // 타겟을 향해 타겟의 거리만큼 레이를 쏴서 장애물이 없다면 타겟을 저장
                target_search.Add(target);
        }

        if (target_search.Count != 0)
        { // 탐색된 오브젝트가 하나라도 있다면
            targeting = target_search[0];
            shortDis = Vector3.Distance(transform.position, target_search[0].transform.position);

            foreach (Transform found in target_search)
            {
                float distance = Vector3.Distance(transform.position, found.transform.position);

                if (distance < shortDis)
                {
                    targeting = found;
                    shortDis = distance;
                }
            }
            Target_Position = targeting;
        }

        else if (target_search.Count == 0)
            Target_Position = SaveTarget_Position;
    }

    void DownPoint()
    {
        GameManager.Instance.point.PointHealth -= 1;
        GameManager.Instance.pointTxt.text = " Health : " + GameManager.Instance.point.PointHealth;
        Destroy(gameObject);
        return;
    }
}
