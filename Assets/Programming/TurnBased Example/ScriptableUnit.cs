using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Create")]
public class ScriptableUnit : ScriptableObject
{
    public string unitName;
    public string unitDescription;
    public string unitAction;
    public int unitLevel;
    public int damage;
    public int maxHealth;
    public float currentHealth;

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
