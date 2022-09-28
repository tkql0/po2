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

    float range = 8f;

    public string PlayerTag = "Player";
    public List<Transform> PlayerList = new List<Transform>();

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
        Collider[] players = Physics.OverlapSphere(transform.position, range, playerMask);

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

                if (dstToTarget <= range)
                {
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = true;
                }

                else if (dstToTarget >= range)
                {
                    players[i].GetComponent<Castle_Spawn>().Point_Spawn_Limit = false;
                }
            }
        }
    }

    public void ReStrat()
    {
        SceneManager.LoadScene(0);
    }


    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
