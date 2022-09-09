using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_set : MonoBehaviour
{
    public Slider sTimer;

    public GameManager manager;

    public float reTime;

    public bool click;

    private void Awake()
    {
        sTimer = GetComponent<Slider>();
    }

    private void Start()
    {
        click = false;
    }

    public void Click()
    {
        if (!click)
            TimerOn();
        else
            TimerOff();
    }

    public void TimerOn()
    {
        click = true;
    }

    public void TimerOff()
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
        Click();
    }
    // 시간 줄어들 때 캐릭터 달리는 이미지 넣고싶다
    // 시간 0되고 다시 리셋할때 캐릭터가 망치로 두드리면서 시간 다시 차게하고
    IEnumerator Return_Strat()
    {
        yield return new WaitForSeconds(5f);
        sTimer.value = reTime;
        manager.StageEnd();
    }
}
