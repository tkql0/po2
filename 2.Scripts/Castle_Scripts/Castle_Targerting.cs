using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Targerting : MonoBehaviour
{
    [SerializeField]
    float Castle_Seatch_Range;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리
    [SerializeField]
    float Castle_Attack_Cool_Time;
    // 공격 쿨타임
    [SerializeField]
    float Castle_damage;

    public GameObject Danger;
    public GameObject Castle_Shoot;

    Transform Target_Position;

    public LayerMask targetMask;
    List<Transform> Castle_Seatch_List = new List<Transform>();
    // 찾은 타겟을 저장할 리스트


    private void Start()
    {
        InvokeRepeating("Castle_Search", 0f, Castle_Attack_Cool_Time);
    }

    void Castle_Search()
    {
        Castle_Seatch_List.Clear();
        // 저장된 타겟을 초기화
        Collider[] inTarget = Physics.OverlapSphere(transform.position, Castle_Seatch_Range, targetMask);
        // 범위안에 들어온 타겟레이어를 저장

        for (int i = 0; i < inTarget.Length; i++)
        {
            Transform target = inTarget[i].transform;
            // i번째 타겟을 대입
            Castle_Seatch_List.Add(target);
        }

        if (Castle_Seatch_List.Count != 0)
        {
            Target_Position = Castle_Seatch_List[0].transform;
            Danger.SetActive(true);
            Shoot();
        }

        else
        {
            Target_Position = null;
            Danger.SetActive(false);
        }
    }

    void Shoot()
    {
        GameObject Attack = Instantiate(Castle_Shoot, Target_Position.GetChild(0).position, Target_Position.rotation);
        Enemy enemy = Target_Position.GetComponent<Enemy>();
        enemy.curHealth = enemy.curHealth - Castle_damage;
        Destroy(Attack, 2f);
    }
}
