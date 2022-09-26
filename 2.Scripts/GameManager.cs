using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleTon<GameManager>
{
    public int stage;
    public Point point;

    public Text stageTxt;
    public Text pointTxt;

    public GameObject Follow_Cam;
    // 게임 매니저에서 시네머신카메라에 할당할 오브젝트를 정하기 위해 공개 설정
    public Transform Enemy_Target;

    public Transform Player_Group;
    public Transform Castle_Group;

    public bool isDefense = false;

    public Player_Spawn player_spawn;
    public Enemy_Spawn enemy_spawn;

    public Camera_Target Camera_target;

    public List<Transform> Castle_List = new List<Transform>();

    public void StageStart()
    {
        isDefense = true;
        stage++;
        player_spawn.Unit_Spawn();
        // 생성 되는 유닛들 저장
        stageTxt.text = stage + " Stage ";
        StartCoroutine(spawn_enemy());
    }

    IEnumerator spawn_enemy()
    {
        for (int i = 0; i < stage; i++)
        {
            enemy_spawn.Spawn_Enemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StageEnd()
    {
        isDefense = false;
        StageStart();
    }
}
