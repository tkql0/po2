using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Targerting : MonoBehaviour
{
    [SerializeField]
    float Enemy_Search_Range;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�
    public NavMeshAgent Enemy_Nav;

    Transform Target_Position;
    // �׺�޽��� ��ǥ������Ʈ
    Transform SaveTarget_Position;
    // ��ǥ�� �Ҿ����� ��ǥ������Ʈ
    Transform targeting;

    Animator Enemy_anim;

    public LayerMask targetMask, obstacleMask;
    // Ÿ�ٰ� ��ֹ�
    float shortDis;

    List<Transform> target_search = new List<Transform>();
    // ã�� Ÿ���� ������ ����Ʈ

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
    { // Enemy ���� �ִϸ��̼��� ���� �� �̵�
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
        { // ������Ʈ�� �����̴� ���϶�
            if (Target_Position == null)
                Target_Position = SaveTarget_Position;

            Enemy_Nav.SetDestination(Target_Position.transform.position);
            Enemy_anim.SetBool("isWalk", true);

            if (Vector3.Distance(transform.position, SaveTarget_Position.transform.position) <= 3f)
                // Ÿ�ٰ��� �Ÿ��� 3��ŭ ��������� ����
                DownPoint();
        }
    }

    void Enemy_Search()
    {
        target_search.Clear();
        // ����� Ÿ���� �ʱ�ȭ
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Enemy_Search_Range, targetMask);
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
             // Ÿ���� ���� Ÿ���� �Ÿ���ŭ ���̸� ���� ��ֹ��� ���ٸ� Ÿ���� ����
                target_search.Add(target);
        }

        if (target_search.Count != 0)
        { // Ž���� ������Ʈ�� �ϳ��� �ִٸ�
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
