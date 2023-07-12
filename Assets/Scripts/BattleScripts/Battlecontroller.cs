using Assets.Scripts.BattleScripts;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class Battlecontroller : MonoBehaviour
{
    public static event Action GameOver;
    public static event Action<int, int> BattleVictory;

    public TMP_Text[] HPBars;
    public GameObject VictoryPanel;
    public GameObject[] InBattleObjects;
    public GameObject Menu;
    public GameObject[] HeroPannels;
    private System.Random rand = new System.Random();
    public GameObject[] EnemyPrefabs;
    public GameObject[] HeroPrefabs;
    GameObject[] Enemies;
    GameObject[] Heroes;
    private int victoryMoney;
    private int victoryXP;
    private bool pause;

    private void Start()
    {
        HeroController.HeroDeath += HeroDeath;
        EnemyController.EnemyDeath += EnemyDeath;
        StartBattle();
        Close();
    }

    public void Open()
    {
        foreach (var hero in Heroes)
        {
            hero.gameObject.SetActive(true);
        }
        foreach (var enemy in Enemies)
        {
            enemy.gameObject.SetActive(true);
        }
        foreach (var BattleObject in InBattleObjects)
        {
            BattleObject.SetActive(true);
        }
        pause = false;
    }

    public void Close()
    {
        foreach (var hero in Heroes)
        {
            hero.gameObject.SetActive(false);
        }
        foreach (var enemy in Enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        foreach (var BattleObject in InBattleObjects)
        {
            BattleObject.SetActive(false);
        }
        pause = true;
    }

    void Update()
    {
        if (!pause)
        {
            for (int i = 0; i < Heroes.Length ; i++)
            {
                var controller = Heroes[i].GetComponent<HeroController>();
                HPBars[i].text = "HP " + controller.HP + "/" + controller.MaxHP;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
                Menu.GetComponent<MainMenu>().OpenMenu();
            }

            if (Enemies.Length == 0)
            {
                BatlleEnd();
            }

            if (Heroes.Length == 0)
            {
                GameOver.Invoke();
            }
        }
    }

    public IEnumerator PlaerTurn()
    {
        for (int i = 0; i < HeroPannels.Length; i++)
        {
            HeroPannels[i].SetActive(true);
        }
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                StartCoroutine(EnemyTurn());
                yield break;
            }
        }

    }

    public void HeroAction(int HeroNumber, int EnemyNumber, string ActionName)
    {
        Debug.Log("Дошло до контроллера");
        HeroController controller = Heroes[HeroNumber].GetComponent<HeroController>();
        controller.MyTurn(Enemies[EnemyNumber], ActionName);
    }

    private IEnumerator EnemyTurn()
    {
        for (int i = 0; i< Enemies.Length; i++)
        {
            EnemyController controller = Enemies[i].GetComponent<EnemyController>();
            Debug.Log("Атаковал юнит с индексом " + i);
            controller.MyTurn(Heroes);
            yield return new WaitForSeconds(2);
        }
        Debug.Log("Ход игрока");
        StartCoroutine(PlaerTurn());
        yield break;
    }

    void HeroDeath(GameObject Hero)
    {
        int index = Array.IndexOf(Heroes, Hero);
        Heroes = Heroes.Where((e, i) => i != index).ToArray();
    }

    void EnemyDeath(GameObject Enemie)
    {
        int index = Array.IndexOf(Enemies, Enemie);
        Enemies = Enemies.Where((e, i) => i != index).ToArray();
    }

    public void StartBattle()
    {
        Enemies = new GameObject[3];
        victoryXP = 0;
        victoryMoney = 0;
        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefabs[rand.Next(EnemyPrefabs.Length)], new  Vector3(gameObject.transform.position.x - 4, gameObject.transform.position.y - 1 + 2.2f * i, gameObject.transform.position.z), new Quaternion(0, 180, 0, 0));
            var controller = enemy.AddComponent<EnemyController>();
            victoryXP += controller.XP;
            victoryMoney += controller.Money;
            Enemies[i] = enemy;
        }
        Heroes = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject hero = Instantiate(HeroPrefabs[i], new Vector3(gameObject.transform.position.x + 8, gameObject.transform.position.y - 1 + 2.2f * i, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0));
            hero.GetComponent<HeroController>().HP = hero.GetComponent<HeroController>().MaxHP;
            Heroes[i] = hero;
        }
        StartCoroutine(PlaerTurn());
    }

    void BatlleEnd()
    {
        foreach (var hero in Heroes)
        {
            HeroDeath(hero);
            Destroy(hero);
        }
        BattleVictory.Invoke(victoryMoney, victoryXP);
        VictoryPanel.GetComponent<VictoryPanelController>().Open(victoryMoney, victoryXP);
        foreach (var hero in HeroPrefabs)
        {
            HeroController HK = hero.GetComponent<HeroController>();
            HK.XP += victoryXP;
        }
        StartBattle();
    }
}
