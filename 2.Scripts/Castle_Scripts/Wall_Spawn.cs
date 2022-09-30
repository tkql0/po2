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
        // 범위안에 들어온 타겟레이어를 저장
        for (int i = 0; i < inTarget.Length; i++)
        { // 저장된 오브젝트 수만큼 반복
            Transform target = inTarget[i].transform;
            // i번째 타겟을 대입
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // 타겟과 자신과의 방향을 저장

            if (dirToTarget != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dirToTarget.normalized);
                float distanceTogameObject = Vector3.Distance(transform.position, target.position);
                // 반복중인 오브젝트와의 거리를 계산해 distanceTogameObject에 대입
                if (distanceTogameObject <= Wall_Range)
                { // distanceTogameObject에 대입한 값이 wallrange보다 작다면 wallrange의 1/2 거리에 wallrange만큼의 길이를 가진 오브젝트를 rot의 방향으로 생성
                    GameObject wall = Instantiate(Wall, new Vector3((transform.position.x + target.position.x) / 2, transform.position.y, (transform.position.z + target.position.z) / 2), rot, spawn_target);
                    wall.transform.localScale += new Vector3(0, 0, distanceTogameObject / 2);
                }
            }
        }
    }
}
