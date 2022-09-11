using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointHealth = 20;
    // 최대 체력
    public int curHealth;
    // 현재 체력

    private void Start()
    {
        curHealth = PointHealth;
    }
}
