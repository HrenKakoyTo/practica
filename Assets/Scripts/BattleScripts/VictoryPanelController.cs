using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryPanelController : MonoBehaviour
{
    public TMP_Text moneyField;
    public TMP_Text XPField;

    public void Open(int MoneyAmont, int XPAmont)
    {
        moneyField.text = MoneyAmont.ToString();
        XPField.text = XPAmont.ToString();
        gameObject.SetActive(true);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
