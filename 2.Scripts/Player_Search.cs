using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Search : MonoBehaviour
{
    public float range = 6f;
    // 타겟을 탐색할 거리 // 레이를 쏠 거리

    public GameObject Danger;

    public LayerMask targetMask;
    // 타겟과 장애물

    Player player;

    public List<Transform> target_search = new List<Transform>();
    // 찾은 타겟을 저장할 리스트

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        StartCoroutine(Update_Search());
    }

    IEnumerator Update_Search()
    {
        while(!player.isDead)
        {
            Search();

        }
        yield return new WaitForSeconds(0.2f);
    }

    void Search()
    {
        target_search.Clear();
        // 저장된 타겟을 초기화
        Collider[] inTarget = Physics.OverlapSphere(transform.position, range, targetMask);
        // 범위안에 들어온 타겟레이어를 저장

        if(inTarget.Length >= 1)
        {
            Danger.SetActive(true);
        }

        else
            Danger.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    { // 그냥 시각적 효과 거리는 5만큼 지워도됨
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
