using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    public float range = 6f;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�

    public GameObject targetPosition;
    public GameObject saveTarget;

    public NavMeshAgent Nav;
    Animator anim;

    public LayerMask targetMask, obstacleMask;
    // Ÿ�ٰ� ��ֹ�

    public List<Transform> target_search = new List<Transform>();
    // ã�� Ÿ���� ������ ����Ʈ

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
        { // Ÿ�ٰ��� �Ÿ��� 3��ŭ ��������� ����
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
