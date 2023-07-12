using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetChoisePanel : MonoBehaviour
{

    public GameObject BattleControllerHolder;
    public int HeroNumber;
    public string ActionName;

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void First()
    {
        SendSignal(0);
    }

    public void Second()
    {
        SendSignal(1);
    }

    public void Third()
    {
        SendSignal(2);
    }

    void SendSignal(int i)
    {
        Debug.Log(HeroNumber + " " + i + " " + ActionName);
        Battlecontroller controller = BattleControllerHolder.GetComponent<Battlecontroller>();
        controller.HeroAction(HeroNumber, i, ActionName);
        ClosePanel();
    }
}
