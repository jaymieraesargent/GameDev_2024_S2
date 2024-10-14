using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBased
{
    public class Unit : MonoBehaviour
    {
        public Sprite unitIcon;
        public string unitName;
        public string unitDescription;
        public string unitAction;
        public int unitLevel;
        public int damage;
        public int maxHealth;
        public float currentHealth;

        //when running TakeDamage pass a damage value in for calculations
        public bool TakeDamage(int damage)
        {
            //current health is affected by damage amount
            currentHealth -= damage;
            //if that kills us
            if (currentHealth <= 0)
            {
                //say that kills us
                return true;
            }
            else
            {
                //else say it didnt kill us
                return false;
            }
        }
        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}
