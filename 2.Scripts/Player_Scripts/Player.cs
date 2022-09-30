using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    // 최대 체력
    float curHealth;
    // 현재 체력

    public int Job_Index;

    bool isDamage;

    public GameObject[] job_Weapons;
    public bool[] hasWeapon;

    bool EDown;

    bool sDown1;
    bool sDown2;
    bool sDown3;

    public bool inshild;
    public bool inbow;

    public bool isDead = false;

    GameObject jobObject;

    public GameObject[] Spawn_Job_Obj;

    public GameObject equipWeapon;

    int equipWeaponIndex = -1;

    Animator Player_Anim;

    public Slider healthSlider;
    // 체력 UI

    Player_Move player_move;

    private void Start()
    {
        Player_Anim = GetComponentInChildren<Animator>();
        player_move = GetComponent<Player_Move>();
        curHealth = maxHealth;
        isDamage = false;

        switch (Job_Index)
        { // 플레이어 생성시 랜덤으로 무기 지급
            case 0:
                break;
            case 1:
                hasWeapon[1] = true;
                break;
            case 2:
                hasWeapon[2] = true;
                break;
        }
    }

    private void Update()
    {
        EDown = Input.GetButtonDown("GetItem");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        Interation();

        if(player_move.enabled == true)
            Swap();
        OnEnable();
    }

    void OnEnable()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = curHealth;
    }

    void Swap()
    {
        if (sDown1 && (!hasWeapon[1] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapon[2] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (equipWeaponIndex == -1))
            return;

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
                    Player_Anim.SetBool("isShild", false);
                    Player_Anim.SetBool("isBow", false);
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
                Player_Anim.SetBool("isShild", false);
                Player_Anim.SetBool("isBow", true);
                inbow = true;
                inshild = false;
            }

            if (sDown2)
            {
                Player_Anim.SetBool("isBow", false);
                Player_Anim.SetBool("isShild", true);
                equipWeapon = job_Weapons[weaponIndex + 1];
                equipWeapon.SetActive(true);
                inbow = false;
                inshild = true;
            }

            if (sDown3)
            {
                Player_Anim.SetBool("isShild", false);
                Player_Anim.SetBool("isBow", false);
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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (!isDamage && !inshild && !enemy.isDead)
            { // 방패가 없다면
                curHealth -= enemy.damage;
                StartCoroutine(OnDamage());
            }

            else if(!isDamage && inshild && !enemy.isDead)
            { // 방패가 있다면
                curHealth -= enemy.damage / 2;
                StartCoroutine(OnDamage());
            }
        }

        if (other.tag == "Weapon")
            // 아이템이 닿고 있다면
            jobObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    { // 아이템을 지나갔다면
        if (other.tag == "Weapon")
            jobObject = null;
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        if (curHealth <= 0 && !isDead)
        {
            Player_Die();
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(1f);
        isDamage = false;
    }

    void Player_Die()
    {
        isDead = true;
        Player_Anim.SetTrigger("doDie");
        GameManager.Instance.player_spawn.Player_Unit_List.Remove(transform);
        GameManager.Instance.Camera_target.Player_Dead();

        if (hasWeapon[1] == true)
            Instantiate(Spawn_Job_Obj[0], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);

        if (hasWeapon[2] == true)
            Instantiate(Spawn_Job_Obj[1], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
    }
}
