using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // �ִ� ü��
    public int curHealth;
    // ���� ü��

    private void Start()
    {
        curHealth = PointHealth;
    }
}
