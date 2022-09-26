using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    float damage = 5;
    float Attack_count = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && Attack_count == 1)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.curHealth -= damage;
            Attack_count = 0;
        }
    }
}
