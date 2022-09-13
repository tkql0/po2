using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Search : MonoBehaviour
{
    public float range = 6f;
    // Ÿ���� Ž���� �Ÿ� // ���̸� �� �Ÿ�

    public GameObject Danger;

    public LayerMask targetMask;
    // Ÿ�ٰ� ��ֹ�

    Player player;

    public List<Transform> target_search = new List<Transform>();
    // ã�� Ÿ���� ������ ����Ʈ

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Search();
    }

    void Search()
    {
        target_search.Clear();
        // ����� Ÿ���� �ʱ�ȭ
        Collider[] inTarget = Physics.OverlapSphere(transform.position, range, targetMask);
        // �����ȿ� ���� Ÿ�ٷ��̾ ����

        if(inTarget.Length >= 1)
        {
            Danger.SetActive(true);
        }

        else
            Danger.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    { // �׳� �ð��� ȿ�� �Ÿ��� 5��ŭ ��������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
