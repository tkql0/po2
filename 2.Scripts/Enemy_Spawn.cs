using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject Enemy_E;
    public GameObject Target;

    public Transform Enemy_Group;

    public void Spawn_Enemy()
    {
        GameObject instantEnemy = Instantiate(Enemy_E, GetRandomPosition(), Quaternion.identity, Enemy_Group);
    }

    public Vector3 GetRandomPosition()
    {
        float radius = 50f;
        Vector3 pointPosition = Target.transform.position;
        float a = pointPosition.x;
        float b = pointPosition.z;

        float x = Random.Range(-radius + a, radius + a);
        float z_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        z_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float z = z_b + b;

        Vector3 randomPosition = new Vector3(x, 1.3f, z);
        return randomPosition;
    }
}
