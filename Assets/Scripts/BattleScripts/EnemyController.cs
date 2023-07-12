using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleScripts
{
    public class EnemyController : MonoBehaviour
    {

        public static event Action<GameObject> EnemyDeath;

        Animator animator;
        private int Dmg = 10;
        private int HP = 100;
        private int MaxHP = 100;
        private int Def = 3;
        public int Money= 15;
        public int XP = 50;
        Renderer[] children;
        void Start()
        {
            animator = GetComponent<Animator>();
            children = gameObject.GetComponentsInChildren<Renderer>();
        }
        void Update()
        {

        }

        public void Attacked(int Damage)
        {

            HP -= (Damage - Def) < 1 ? 1 : (Damage - Def);
            if (HP < 0) Death();
            Debug.Log(gameObject.name + "Получено " + Damage + " урона. Осталось" + HP + "HP.");
            StartCoroutine("Blinking");
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


        public void MyTurn(GameObject[] enemies)
        {
            Debug.Log(enemies.Length);
            GameObject target = enemies[UnityEngine.Random.Range(0, enemies.Length)];
            var TargetController = target.GetComponent<HeroController>();
            TargetController.Attacked(Dmg);
            animator.SetTrigger("attack");
            Debug.Log("Нанесено " + Dmg + "урона, цель-" + target.name);
        }

        void Death()
        {
            Debug.Log(gameObject.name + "Убит.");
            EnemyDeath.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}