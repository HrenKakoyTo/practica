using System;
using UnityEngine;

public class StatusMenu : MonoBehaviour
{
    public static event Action Shutdown;

    private void Start()
    {
        CloseMenu();
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Shutdown.Invoke();
    }
}
