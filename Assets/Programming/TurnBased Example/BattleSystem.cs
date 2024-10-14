using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace TurnBased
{
    public class BattleSystem : MonoBehaviour
    {
        [Header("Player")]
        public GameObject playerPrefab;
        public Transform playerBattleStation;
        public Unit playerUnit;
        public BattleHUD playerHUD;
        [Header("Enemy")]
        public GameObject enemyPrefab;
        public Transform enemyBattleStation;
        public Unit enemyUnit;
        public BattleHUD enemyHUD;

        public Text dialogueText;
        public BattleState battleState;


        private void Start()
        {
            battleState = BattleState.StartBattle;
            StartCoroutine(SetupBattle());
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(enemyUnit);

        }
        void PlayerTurn()
        {
            dialogueText.text = "Choose Action...";
        }
        public void OnAttack()
        {
            if (battleState != BattleState.PlayerTurn)
            {
                return;
            }
            StartCoroutine(PlayerAttack());
        }
        public void OnHeal()
        {
            if (battleState != BattleState.PlayerTurn)
            {
                return;
            }
            StartCoroutine(PlayerHeal());
        }
        void EndBattle()
        {
            if (battleState == BattleState.Win)
            {
                dialogueText.text = $"You Won the battle by defeating {enemyUnit.unitDescription} {enemyUnit.unitName}";
            }
            else if (battleState == BattleState.Lose)
            {
                dialogueText.text = $"You Lost the battle and were defeated by {enemyUnit.unitDescription} {enemyUnit.unitName}";
            }
        }
        IEnumerator SetupBattle()
        {
            GameObject player = Instantiate(playerPrefab, playerBattleStation);
            playerUnit = player.GetComponent<Unit>();
            GameObject enemy = Instantiate(enemyPrefab, enemyBattleStation);
            enemyUnit = enemy.GetComponent<Unit>();
            dialogueText.text = $"{enemyUnit.unitDescription} {enemyUnit.unitName} {enemyUnit.unitAction}...";
            yield return new WaitForSeconds(2);
            battleState = BattleState.PlayerTurn;
            PlayerTurn();
        }
        IEnumerator PlayerAttack()
        {
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
            enemyHUD.SetHealth(enemyUnit);
            dialogueText.text = $"{playerUnit.unitName} attacked {enemyUnit.unitName}";
            yield return new WaitForSeconds(2);
            if (isDead)
            {
                battleState = BattleState.Win;
                EndBattle();
            }
            else
            {
                battleState = BattleState.EnemyTurn;
                StartCoroutine(EnemyTurn());
            }
        }
        IEnumerator PlayerHeal()
        {
            playerUnit.Heal(2);
            playerHUD.SetHealth(playerUnit);
            dialogueText.text = $"{playerUnit.unitName} feels stronger!";
            yield return new WaitForSeconds(2);
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
        IEnumerator EnemyTurn()
        {
            dialogueText.text = $"{enemyUnit.unitName} Attacks!!!";
            yield return new WaitForSeconds(1);
            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHUD.SetHealth(playerUnit);
            yield return new WaitForSeconds(1);
            if (isDead)
            {
                battleState = BattleState.Lose;
                EndBattle();
            }
            else
            {
                battleState = BattleState.PlayerTurn;
                PlayerTurn();
            }
        }






    }
    public enum BattleState
    {
        StartBattle,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }
}
