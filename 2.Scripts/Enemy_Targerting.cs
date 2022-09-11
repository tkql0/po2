using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    public float range = 6f;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리

    public GameObject targetPosition;
    public GameObject saveTarget;

    public NavMeshAgent Nav;
    Animator anim;

    public LayerMask targetMask, obstacleMask;
    // 타겟과 장애물

    public List<Transform> target_search = new List<Transform>();
    // 찾은 타겟을 저장할 리스트

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
        saveTarget = targetPosition;
    }

    private void Update()
    {
        StartCoroutine(InstantiateEnemy());
        if (Vector3.Distance(transform.position, saveTarget.transform.position) <= 3f)
        { // 타겟과의 거리가 3만큼 가까워지면 삭제
            DownPoint();
            return;
        }
        Search();
    }

    IEnumerator InstantiateEnemy()
    {
        yield return new WaitForSeconds(4f);
        if (Nav.enabled)
        {
            Nav.SetDestination(targetPosition.transform.position);
            anim.SetBool("isWalk", true);
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
            { // 타겟을 향해 타겟의 거리만큼 레이를 쏴서 장애물이 없다면 타겟을 저장
                target_search.Add(target);
            }
        }
    }

    void DownPoint()
    {
        GameManager.Instance.point.PointHealth -= 1;
        GameManager.Instance.pointTxt.text = " Health : " + GameManager.Instance.point.PointHealth;
        Destroy(this.gameObject);

    }

    private void OnDrawGizmosSelected()
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
