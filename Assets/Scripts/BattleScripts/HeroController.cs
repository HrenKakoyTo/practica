using Assets.Scripts.BattleScripts;
using System;
using System.Collections;
using UnityEngine;

public class HeroController : MonoBehaviour
{

    public static event Action<GameObject> HeroDeath;

    private readonly System.Random rand;
    public int Dmg = 10;
    public int HP = 100;
    public int MaxHP = 100;
    private int Def = 3;
    public int XP = 0;
    public int lvl = 1;
    Renderer[] children;
    public bool inDef = false;

    void Start()
    {
        children = gameObject.GetComponentsInChildren<Renderer>();
    }
    void Update()
    {
        if (XP > lvl * 100)
        {
            lvlUp();
        }
    }

    public void Attacked(int Damage)
    {
        HP -= (Damage - Def) < 1 ? 1 : (Damage - Def);
        Debug.Log(gameObject.name + "Получено " + Damage + " урона. Осталось" + HP + "HP.");
        if (HP < 0) Death();
        StartCoroutine("Blinking");
    }

    public void MyTurn(GameObject target, string ActionName)
    {
        Debug.Log("Дошло до героя");
        if (ActionName == "attack")
        {
            var TargetController = target.GetComponent<EnemyController>();
            TargetController.Attacked(Dmg);
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("attack");
        }
        if (ActionName == "defence")
        {

        }
    }

    IEnumerator Blinking()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < children.Length; j++)
            {
                children[j].enabled = false;
            }
            yield return new WaitForSeconds(0.2f);
            for (int j = 0; j < children.Length; j++)
            {
                children[j].enabled = true;
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    void Death()
    {
        Debug.Log(gameObject.name + "Убит.");
        HeroDeath.Invoke(gameObject);
        Destroy(gameObject);
    }

    void lvlUp()
    {
        XP = XP - (lvl * 100);
        lvl += 1;
        Dmg += 1 + (lvl/5);
        MaxHP += 5 * lvl;
        HP = MaxHP;
        Def += 1;
        if (Def > 7) {
            Def = 7;
        }
    }
}