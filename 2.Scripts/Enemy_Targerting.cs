using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    public float range = 6f;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�

    public Transform targetPosition;
    public Transform saveTarget;

    public Transform targeting;

    public NavMeshAgent Nav;
    Animator anim;

    Enemy enemy;

    public LayerMask targetMask, obstacleMask;
    // Ÿ�ٰ� ��ֹ�

    public float shortDis;

    public List<Transform> target_search = new List<Transform>();
    // ã�� Ÿ���� ������ ����Ʈ

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(InstantiateEnemy());
        Nav = GetComponent<NavMeshAgent>();
        saveTarget = targetPosition;
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Nav.SetDestination(targetPosition.transform.position);
        anim.SetBool("isWalk", true);
        Search();
        if (Vector3.Distance(transform.position, saveTarget.transform.position) <= 3f)
        { // Ÿ�ٰ��� �Ÿ��� 3��ŭ ��������� ����
            DownPoint();
            return;

        }
    }

    IEnumerator InstantiateEnemy()
    {
        yield return new WaitForSeconds(4f);
        Nav.SetDestination(targetPosition.transform.position);
    }

    void Search()
    {
        target_search.Clear();
        // ����� Ÿ���� �ʱ�ȭ
        Collider[] inTarget = Physics.OverlapSphere(transform.position, range, targetMask);
        // �����ȿ� ���� Ÿ�ٷ��̾ ����

        for (int i = 0; i < inTarget.Length; i++)
        {
            Transform target = inTarget[i].transform;
            // i��° Ÿ���� ����
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // Ÿ�ٰ� �ڽŰ��� ������ ����
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
            // Ÿ�ٰ� �ڽŰ��� �Ÿ��� ����
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            { // Ÿ���� ���� Ÿ���� �Ÿ���ŭ ���̸� ���� ��ֹ��� ���ٸ� Ÿ���� ����
                target_search.Add(target);
            }
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
        WayPoint();
    }

    void WayPoint()
    {
        if (Nav.isPathStale)
        {
            targetPosition = GameManager.Instance.Castle_List[GameManager.Instance.Castle_List.Count].transform;
            return;
        }
    }

    void DownPoint()
    {
        GameManager.Instance.point.PointHealth -= 1;
        GameManager.Instance.pointTxt.text = " Health : " + GameManager.Instance.point.PointHealth;
        Destroy(this.gameObject);

    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
