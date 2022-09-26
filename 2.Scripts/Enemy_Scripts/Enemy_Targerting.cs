using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    [SerializeField]
    float range;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리
    public NavMeshAgent Nav;

    Transform targetPosition;
    Transform saveTarget;
    Transform targeting;

    Animator anim;

    public LayerMask targetMask, obstacleMask;
    // 타겟과 장애물
    float shortDis;

    List<Transform> target_search = new List<Transform>();
    // 찾은 타겟을 저장할 리스트

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        Nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(InstantiateEnemy());
        targetPosition = GameManager.Instance.Enemy_Target;
        saveTarget = targetPosition;
        StartCoroutine(Targeing_tDelay(0.2f));
    }

    IEnumerator InstantiateEnemy()
    { // Enemy 생성 애니메이션이 끝난 후 네비메쉬 목표설정
        yield return new WaitForSeconds(5f);
        Nav.SetDestination(targetPosition.transform.position);
    }

    IEnumerator Targeing_tDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Search();
        }
    }

    void Update()
    {
        if (Nav.enabled)
        {
            if ( targetPosition == null)
                targetPosition = saveTarget;
            Nav.SetDestination(targetPosition.transform.position);
            anim.SetBool("isWalk", true);
            if (Vector3.Distance(transform.position, saveTarget.transform.position) <= 3f)
            { // 타겟과의 거리가 3만큼 가까워지면 삭제
                DownPoint();
                return;
            }
        }
    }

    void Search()
    {
        target_search.Clear();
        // 저장된 타겟을 초기화
        Collider[] inTarget = Physics.OverlapSphere(transform.position, range, targetMask);
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
        {
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
            targetPosition = targeting;
        }
        else if (target_search.Count == 0)
        {
            targetPosition = saveTarget;
        }
    }

    void DownPoint()
    {
        GameManager.Instance.point.PointHealth -= 1;
        GameManager.Instance.pointTxt.text = " Health : " + GameManager.Instance.point.PointHealth;
        Destroy(gameObject);
    }
}
