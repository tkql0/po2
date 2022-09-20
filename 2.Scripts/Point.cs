using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // �ִ� ü��
    public int curHealth;
    // ���� ü��
    public LayerMask targetMask;

    float range = 8f;

    private void Start()
    {
        curHealth = PointHealth;
    }

    private void Update()
    {
        Spawn_Limit_Target();
    }

    void Spawn_Limit_Target()
    {
        
    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
