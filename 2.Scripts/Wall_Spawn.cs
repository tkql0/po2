using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Spawn : MonoBehaviour
{
    // 타겟 태그 Castle 추가
    // 일정 범위 안에 들어오면 사이에 생성
    // 드래그 클래스 참조해서 성 사이의 길이만큼 늘어나게 하기

    public string CastleTag = "Castle";

    public Transform spawn_target;

    public float wallrange = 1000f;

    public GameObject Wall;
    // 성과 성 사이에 생성
    // 다른 성과의 각도와 거리를 계산하고 거리의 1/2만큼의 거리에 생성
    // 범위를 2정도 주고 그 안에 있다면 있는 수만큼 반복 // start함수로 해야겠다 // 생성과 동시에 주변오브젝트를 탐색하고 벽 생성

    private void Start()
    {
        spawn_target = this.transform;
        GameObject[] castles = GameObject.FindGameObjectsWithTag(CastleTag);
        // 같은 태그의 오브젝트를 저장
        foreach (GameObject Castle in castles)
        { // 저장된 오브젝트 수만큼 반복
            Vector3 dir = Castle.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            float distanceTogameObject = Vector3.Distance(transform.position, Castle.transform.position);
            // 반복중인 오브젝트와의 거리를 계산해 distanceTogameObject에 대입
            if (distanceTogameObject <= wallrange)
            { // distanceTogameObject에 대입한 값이 wallrange보다 작다면 wallrange의 1/2 거리에 wallrange만큼의 길이를 가진 오브젝트를 rot의 방향으로 생성
                GameObject wall = Instantiate(Wall, new Vector3((transform.position.x + Castle.transform.position.x) / 2, transform.position.y, (transform.position.z + Castle.transform.position.z) / 2), rot, spawn_target);
                wall.transform.localScale += new Vector3(0, 0, distanceTogameObject/2);
                // 성 밑으로 생성되게 해서 성이 파괴될시 같이 파괴
                // 애니메이션을 넣는다면 플레이어가 걸어가는 모습을 생성된 방향으로 하기
                // 
            }
        }
    }
}
