using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // �ִ� ü��
    public LayerMask playerMask;

    public GameObject panel;

    float Point_Limit_Range = 8f;

    List<Transform> PlayerList = new List<Transform>();

    private void Update()
    {
        if (PointHealth < 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        Spawn_Limit_Target();
    }

    void Spawn_Limit_Target()
    {
        PlayerList.Clear();
        Collider[] players = Physics.OverlapSphere(transform.position, Point_Limit_Range, playerMask);

        for (int i = 0; i < players.Length; i++)
        { // �����ȿ� �ִ� �÷��̾ �޾ƿͼ� ����
            Transform target = players[i].transform;
            // i��° Ÿ���� ����
            PlayerList.Add(target);
        }

        if (PlayerList.Count != 0)
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                float dstToTarget = Vector3.Distance(transform.position, players[i].transform.position);
                // �ݺ����� ������Ʈ���� �Ÿ��� ����� distanceTogameObject�� ����

                if (dstToTarget <= Point_Limit_Range)
                    // Player�� Point�� �Ÿ��� ���ѹ������� �����ٸ�
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = true;

                else if (dstToTarget >= Point_Limit_Range)
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = false;
            }
        }
    }

    public void ReStrat()
    {
        SceneManager.LoadScene(0);
    }
}
