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
    public OnHealthChange onSPChange;
    public OnHealthChange onDeath;

    public List<Effects> effects;

    private void Start()
    {
        updateMaxHealth();
        healToFull();
        updateMaxSP();
        spToFull();
        effects = new List<Effects>();
    }

    public void init()
    {
        Start();
    }

    public float getCurrentHealthPercent()
    {
        return currentHealth / (float)maxHealth;
    }
    public float getCurrentSPPercent()
    {
        return currentSP / (float)maxSP;
    }

    public void healToFull()
    {
        currentHealth = maxHealth;
        if(onHealthChange != null)
        {
            onHealthChange(this);
        }
    }

    public void changeCurrentSP(int drain)
    {
        currentSP -= drain;
        if(onSPChange != null)
        {
            onSPChange(this);
        }
    }

    public void spToFull()
    {
        currentSP = maxSP;
        if(onSPChange != null)
        {
            onSPChange(this);
        }
    }

    public void updateMaxHealth()
    {
        maxHealth = character.stats.constitution * character.stats.healthPerConstitution;
    }

    public void updateMaxSP()
    {
        maxSP = character.stats.wisdom * character.stats.spPerWisdom;
    }

    public void applyEffect(Effects effect)
    {
        if(effect.onApply(this))
            effects.Add(effect);
    }

    public IEnumerator resolveEffects()
    {
        for(int i =0; i < effects.Count; i++)
        {
            Effects effect = effects[i];
            effect.onTurnUpdate();
            if(effect.displayInText)
            {
                CombatLogger.instance.logEffectString(effect.getLogText());
            }
            yield return CombatLogger.instance.isDisplaying();
        }
    }

    public virtual void takeDamage(int damage)
    {
        if (damage < 0) { damage = 0; }
        currentHealth -= damage;
        if(onHealthChange != null)
        {
            onHealthChange(this);
        }
        if(currentHealth <= 0)
        {
            death();
        }
    }

    public virtual void death()
    {
        if(onDeath != null)
        {
            onDeath(this);
        }
    }

    public void clearStatuses()
    {
        effects.Clear();
    }

    public CombatResults attemptDamage(int damage, int atkStat, int critBonusRoll)
    {
        CombatResults retval = new CombatResults();
        int dRoll = D.R20();
        if(dRoll == 1) { retval.miss = true; return retval; }
        int atkRoll = atkStat + dRoll;
        Debug.Log("Attack Roll: " + atkRoll);
        
        if (atkRoll > character.stats.agility || dRoll == 20)
        {
            bool crit = D.R20() >= 20 - critBonusRoll;
            
            //if(log)
              //  CombatLogger.instance.logCombatString(attacker, character, ability, false, damage, crit);
            retval.crit = crit;
            Debug.Log("Hit");
            return retval;
        }

        //CombatLogger.instance.logCombatString(attacker, character, ability, true, 0, false);
        retval.miss = true;
        Debug.Log("Miss");
        return retval;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}

public class CombatResults
{
    public bool miss;
    public bool crit;
}

