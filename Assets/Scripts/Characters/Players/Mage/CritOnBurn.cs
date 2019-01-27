using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritOnBurn : CharacterAbility
{
    public float damagerPerIntellect;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        return "Deals " + damage + " to an enemy. Always crits if enemy is burning.";
    }
    public override string getAttribute()
    {
        return "INT";
    }
    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        CombatResults result = target.health.attemptDamage(damage, character.stats.intellect, character.stats.critBonusRoll);
        if(!result.miss)
        {
            if (!result.crit) { result.crit = targetHasBurn(target); }
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
        }
        sendCombatLog(result,target, damage);
    }



    bool targetHasBurn(Character target)
    {
        for(int i =0; i < target.health.effects.Count; i++)
        {
            if(target.health.effects[i] is Burn)
            {
                return true;
            }
        }
        return false;
    }
}
