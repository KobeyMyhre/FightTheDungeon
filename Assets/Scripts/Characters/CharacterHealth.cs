using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHealthChange(CharacterHealth health);

public class CharacterHealth : MonoBehaviour
{
    public Character character;
    public int maxHealth;
    public int currentHealth;
    public int maxSP;
    public int currentSP;
    public OnHealthChange onHealthChange;

    private void Start()
    {
        updateMaxHealth();
        healToFull();
        updateMaxSP();
        spToFull();
    }

    public float getCurrentHealthPercent()
    {
        return currentHealth / (float)maxHealth;
    }

    public void healToFull()
    {
        currentHealth = maxHealth;
    }

    public void spToFull()
    {
        currentSP = maxSP;
    }

    public void updateMaxHealth()
    {
        maxHealth = character.stats.constitution * character.stats.healthPerConstitution;
    }

    public void updateMaxSP()
    {
        maxSP = character.stats.willpower * character.stats.spPerWillpower;
    }


    public virtual void takeDamage(int damage)
    {
        if (damage < 0) { damage = 0; }
        currentHealth -= damage;
        if(onHealthChange != null)
        {
            onHealthChange(this);
        }
    }

    public bool attemptDamage(int damage, int atkStat)
    {
        int dRoll = D.R20();
        if(dRoll == 1) { return false; }
        int atkRoll = atkStat + dRoll ;
        Debug.Log("Attack Roll: " + atkRoll);
        
        if (atkRoll > character.stats.agility || dRoll == 20)
        {
            bool crit = D.R20() == 20;
            damage = crit ? damage * 2 : damage;
            takeDamage(damage);
            Debug.Log("Hit");
            return true;
        }
        Debug.Log("Miss");
        return false;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}
