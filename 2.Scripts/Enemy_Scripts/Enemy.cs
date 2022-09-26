using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    public float curHealth;

    public float damage;

    public bool isDead = false;

    Animator anim;

    public Slider healthSlider;
    // Ã¼·Â UI

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        OnEnable();
        StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage()
    {
        if (curHealth <= 0 && !isDead)
        {
            Enemy_Die();
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(1f);
    }

    void OnEnable()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = curHealth;
    }

    void Enemy_Die()
    {
        isDead = true;
        anim.SetTrigger("doDie");
        Enemy_Targerting enemy_Targerting = GetComponent<Enemy_Targerting>();
        enemy_Targerting.Nav.isStopped = true;
    }
}
