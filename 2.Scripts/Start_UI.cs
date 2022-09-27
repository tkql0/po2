using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_UI : MonoBehaviour
{
    public GameManager manager;

    public GameObject Panel;

    public void OnClick()
    {
        if (Panel == true)
            Panel.SetActive(false);
        manager.StageStart();
        Time.timeScale = 1;
    }
}
