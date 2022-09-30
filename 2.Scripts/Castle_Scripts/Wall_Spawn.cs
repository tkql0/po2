using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Spawn : MonoBehaviour
{
    public LayerMask targetMask;

    Transform spawn_target;

    [SerializeField]
    float Wall_Range = 4f;

    public GameObject Wall;

    private void Start()
    {
        spawn_target = this.transform;
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Wall_Range, targetMask);
        // �����ȿ� ���� Ÿ�ٷ��̾ ����
        for (int i = 0; i < inTarget.Length; i++)
        { // ����� ������Ʈ ����ŭ �ݺ�
            Transform target = inTarget[i].transform;
            // i��° Ÿ���� ����
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // Ÿ�ٰ� �ڽŰ��� ������ ����

            if (dirToTarget != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dirToTarget.normalized);
                float distanceTogameObject = Vector3.Distance(transform.position, target.position);
                // �ݺ����� ������Ʈ���� �Ÿ��� ����� distanceTogameObject�� ����
                if (distanceTogameObject <= Wall_Range)
                { // distanceTogameObject�� ������ ���� wallrange���� �۴ٸ� wallrange�� 1/2 �Ÿ��� wallrange��ŭ�� ���̸� ���� ������Ʈ�� rot�� �������� ����
                    GameObject wall = Instantiate(Wall, new Vector3((transform.position.x + target.position.x) / 2, transform.position.y, (transform.position.z + target.position.z) / 2), rot, spawn_target);
                    wall.transform.localScale += new Vector3(0, 0, distanceTogameObject / 2);
                }
            }
        }
    }
}
