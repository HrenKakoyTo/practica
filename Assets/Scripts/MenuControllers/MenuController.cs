using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        CloseMenu();
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
