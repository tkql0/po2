using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    public float range = 5f;

    public GameObject targetPosition;

    public NavMeshAgent Nav;
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        StartCoroutine(InstantiateEnemy());
    }

    IEnumerator InstantiateEnemy()
    {
        yield return new WaitForSeconds(4f);
        if (Nav.enabled)
        {
            Nav.SetDestination(targetPosition.transform.position);
            anim.SetBool("isWalk", true);
        }
    }

            private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
