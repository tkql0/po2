using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleTon<GameManager>
{
    public int stage;

    public GameObject Follow_Cam;
    // 게임 매니저에서 시네머신카메라에 할당할 오브젝트를 정하기 위해 공개 설정
    // 할당한 오브젝트에서만 마우스 이동 활성화 시키기

    public Transform Player_Group;
    // 플레이어가 생성될 시 들어갈 그룹

    public Transform Castle_Group;

    public bool isDefense = false;

    public Player_Spawn player_spawn;
    public Enemy_Spawn enemy_spawn;

    public Player_Move player_Move;

    public Text stageTxt;

    private void Start()
    {
        StageStart();

        Time.timeScale = 1;
    }

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
