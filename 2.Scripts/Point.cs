using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // �ִ� ü��
    public LayerMask targetMask;

    public GameObject panel;

    float range = 8f;

    public string PlayerTag = "Player";
    public List<GameObject> PlayerList = new List<GameObject>();

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
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        foreach (GameObject Player in players)
        {
            Vector3 dir = Player.transform.position - transform.position;
            if (dir != Vector3.zero && players.Length > 0)
            {
                float distanceTogameObject = Vector3.Distance(transform.position, Player.transform.position);
                // �ݺ����� ������Ʈ���� �Ÿ��� ����� distanceTogameObject�� ����
                if (distanceTogameObject <= range)
                {
                    Player.GetComponent<Castle_Spawn>().Spawn_Limit = true;
                }
                else
                {
                    Player.GetComponent<Castle_Spawn>().Spawn_Limit = false;
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
