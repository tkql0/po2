using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy_Targerting : MonoBehaviour
{
    public float range = 5f;

    public GameObject targetPosition;
    public GameObject saveTarget;

    public NavMeshAgent Nav;
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
        saveTarget = targetPosition;
    }
    private void Update()
    {
        StartCoroutine(InstantiateEnemy());
        if (Vector3.Distance(transform.position, saveTarget.transform.position) <= 3f)
        {
            DownPoint();
            return;
        }
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

    void DownPoint()
    {
        GameManager.Instance.point.PointHealth -= 1;
        GameManager.Instance.pointTxt.text = " Health : " + GameManager.Instance.point.PointHealth;
        Destroy(this.gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
