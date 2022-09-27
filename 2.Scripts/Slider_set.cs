using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_set : MonoBehaviour
{
    Slider sTimer;
    float reTime = 15;

    public GameManager manager;

    bool click;

    private void Awake()
    {
        sTimer = GetComponent<Slider>();
    }

    private void Start()
    {
        click = false;
    }

    private void Update()
    {
        if(click)
        {
            if (sTimer.value > 0.0f)
            {
                sTimer.value -= Time.deltaTime;

                if (sTimer.value <= 0.0f)
                    StartCoroutine(Return_Strat());
            }
        }
    }

    public void Click()
    {
        if (!click)
            click = true;

        else
            click = false;
    }

    IEnumerator Return_Strat()
    {
        yield return new WaitForSeconds(4f);
        sTimer.value = reTime;
        manager.StageEnd();
    }
}
