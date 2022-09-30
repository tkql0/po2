using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField]
    float Player_Damage;
    float Player_Attack_Count = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && Player_Attack_Count == 1)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.curHealth -= Player_Damage;
            Player_Attack_Count = 0;
        }
    }
}
