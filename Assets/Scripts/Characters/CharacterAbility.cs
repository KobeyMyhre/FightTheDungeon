using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{

    public Character character;
    public string abilityName;
    [TextArea]
    public string abiltyDescription;
    public int spCost;
    public virtual void useAbilty(Character target)
    {
        character.health.changeCurrentSP(spCost);
    }
    
    public virtual void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = character.name + " used " + abilityName + ".";
        string log2 = "";
        if (result.miss) { log2 += "It Missed..."; }
        else
        {
            if (result.crit) { log2 += "Critical Hit! "; }
            log2 += target.name + " takes " + damage + " damage!";
        }
        CombatLogger.instance.logCombatString(log, log2);
    }

    protected int getRoundedDamage(float damagePer, int stat)
    {
        return Mathf.RoundToInt(stat * damagePer);
    }

    public virtual string getDescription()
    {
        return abiltyDescription;
    }
    public bool hasEnoughSP()
    {
        if(character.health.currentSP >= spCost)
        {
            return true;
        }
        return false;
    }
}
