using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int Money;
    public TMP_Text MoneyText;
    public TMP_Text EndText;
    public GameObject EndPanel;

    private void Start()
    {
        Battlecontroller.GameOver += BadEnd;
        Money = 100;
        Battlecontroller.BattleVictory += MoneyUpdate;
    }
    void Update()
    {
        if (Money >= 200)
        {
            HappyEnd();
        }
    }

    void MoneyUpdate(int money, int xp = 0)
    {
        Money += money;
        MoneyText.text = "G " + Money.ToString(); 
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HappyEnd()
    {
        EndPanel.SetActive(true);
        EndText.text = "Victory";
    }

    public void BadEnd()
    {
        EndPanel.SetActive(true);
        EndText.text = "GameOver";
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
