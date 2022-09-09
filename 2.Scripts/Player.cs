using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int count = 0;
    public int maxHealth;
    // 최대 체력
    public int curHealth;
    // 현재 체력
    public bool isDamage;

    //public int job_count;
    // 생성시 랜덤한 직업을 가질 카운트

    public GameObject[] job_Weapons;
    public bool[] hasWeapon;

    bool EDown;

    bool sDown1;
    bool sDown2;
    bool sDown3;

    public bool inshild;
    public bool inbow;

    bool isDead;

    GameObject jobObject;

    public GameObject equipWeapon;

    int equipWeaponIndex = -1;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        curHealth = maxHealth;
        isDamage = false;
    }

    private void Update()
    {
        EDown = Input.GetButtonDown("GetItem");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        Interation();
        Swap();
    }

    void Swap()
    {
        if (sDown1 && (!hasWeapon[1] || equipWeaponIndex == 0))
        {
            return;
        }
        if (sDown2 && (!hasWeapon[2] || equipWeaponIndex == 1))
        {
            return;
        }
        if (sDown3 && (equipWeaponIndex == -1))
        {
            return;
        }
        int weaponIndex = -1;

        if (sDown1 || sDown2 || sDown3)
        {
            if (sDown1) weaponIndex = 0;
            if (sDown2) weaponIndex = 1;
            if (sDown3) weaponIndex = 3;

            if (equipWeapon != null)
            {
                equipWeapon.SetActive(false);

                if (sDown1)
                {
                    equipWeapon = job_Weapons[weaponIndex + 1];
                    equipWeapon.SetActive(false);
                    weaponIndex = 0;
                }
                if (sDown2)
                {
                    weaponIndex = 1;
                }
                if (sDown3)
                {
                    anim.SetBool("isShild", false);
                    anim.SetBool("isBow", false);
                    for (int i = 0; i <= 2; i++)
                    {
                        if (job_Weapons[i].activeSelf == true)
                        {
                            equipWeapon = job_Weapons[i];
                            equipWeapon.SetActive(false);
                        }
                    }
                    weaponIndex = 3;
                }
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = job_Weapons[weaponIndex];

            equipWeapon.SetActive(true);
            if (sDown1)
            {
                anim.SetBool("isShild", false);
                anim.SetBool("isBow", true);
                inbow = true;
                inshild = false;
            }
            if (sDown2)
            {
                anim.SetBool("isBow", false);
                anim.SetBool("isShild", true);
                equipWeapon = job_Weapons[weaponIndex + 1];
                equipWeapon.SetActive(true);
                inbow = false;
                inshild = true;
            }
            if (sDown3)
            {
                anim.SetBool("isShild", false);
                anim.SetBool("isBow", false);
                equipWeapon = job_Weapons[weaponIndex];
                equipWeapon.SetActive(true);
                inbow = false;
                inshild = false;
            }
        }
    }

    void Interation()
    {
        hasWeapon[0] = true;
        if (EDown && jobObject != null)
        {
            if(jobObject.tag == "Weapon")
            {
                Item item = jobObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapon[weaponIndex] = true;

                Destroy(jobObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!isDamage)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                curHealth -= enemy.damage;
                StartCoroutine(OnDamage());
            }
        }
    }

        public void ondamage()
    {
        //StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        if (curHealth <= 0 && !isDead)
        {
            Player_Die();
            StopCoroutine("OnDamage");
        }
            yield return new WaitForSeconds(1f);
        isDamage = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            jobObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            jobObject = null;
        }
    }

    void Player_Die()
    {
        
            anim.SetTrigger("doDie");
            isDead = true;
    }
}
