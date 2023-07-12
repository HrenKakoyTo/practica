using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;

    private void Start()
    {
        StatusMenu.Shutdown += OpenMenu;
        ItemMenu.Shutdown += OpenMenu;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void OpenMenu()
    {
        for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        gameObject.SetActive(true);
    }
}