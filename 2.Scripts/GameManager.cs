using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleTon<GameManager>
{
    public int stage;

    public GameObject Follow_Cam;
    // ���� �Ŵ������� �ó׸ӽ�ī�޶� �Ҵ��� ������Ʈ�� ���ϱ� ���� ���� ����
    // �Ҵ��� ������Ʈ������ ���콺 �̵� Ȱ��ȭ ��Ű��

    public Transform Player_Group;
    // �÷��̾ ������ �� �� �׷�

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
        // ���� �Ǵ� ���ֵ� ����
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
