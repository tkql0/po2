using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleTon<GameManager>
{
    int stage;
    public Point point;

    public Text stageTxt;
    public Text pointTxt;

    public GameObject Follow_Cam;
    // ���� �Ŵ������� �ó׸ӽ�ī�޶� �Ҵ��� ������Ʈ�� ���ϱ� ���� ���� ����
    public Transform Enemy_Target;

    public Transform Player_Group;
    public Transform Castle_Group;

    public Player_Spawn player_spawn;
    public Enemy_Spawn enemy_spawn;

    public Camera_Target Camera_target;

    public List<Transform> Castle_List = new List<Transform>();

    public Slider Point_healthSlider;

    private void Update()
    {
        Point_healthSlider.maxValue = 20;
        Point_healthSlider.value = point.PointHealth;
    }

    public void StageStart()
    {
        stage++;
        player_spawn.Player_Unit_Spawn();
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
}
