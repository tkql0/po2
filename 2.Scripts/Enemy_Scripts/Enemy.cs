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

    Animator Enemy_anim;

    public Slider healthSlider;

    private void Awake()
    {
        Enemy_anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        OnEnable();
        StartCoroutine(Enemy_Die());
    }

    IEnumerator Enemy_Die()
    {
        if (curHealth <= 0 && !isDead)
        {
            isDead = true;
            Enemy_anim.SetTrigger("doDie");
            Enemy_Targerting enemy_Targerting = GetComponent<Enemy_Targerting>();
            enemy_Targerting.Enemy_Nav.isStopped = true;
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
}
