using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    // 최대 체력
    public int curHealth;
    // 현재 체력
    public int damage = 2;

    public bool isDead = false;

    Animator anim;

    //public Slider healthSlider;
    // 체력 UI

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    IEnumerator OnDamage()
    {
        if (curHealth <= 0 && !isDead)
        {
            Enemy_Die();
            isDead = true;
            StopCoroutine("OnDamage");
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(1f);
    }

    void Enemy_Die()
    {
        anim.SetTrigger("doDie");
        isDead = true;
        Enemy_Targerting enemy_Targerting = GetComponent<Enemy_Targerting>();
        enemy_Targerting.Nav.enabled = false;
    }
}
