using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // 최대 체력
    public int curHealth;
    // 현재 체력
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
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
