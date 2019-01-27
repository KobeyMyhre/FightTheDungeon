using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSP : EnemyAbility
{
    public float drainPerWisdom;
    public float damagePerIntellect;

    public override void useAbilty(Character target)
    {
        int drainSP = getRoundedDamage(drainPerWisdom, character.stats.wisdom);
        int damage = getRoundedDamage(damagePerIntellect, character.stats.intellect);

        CombatResults result = target.health.attemptDamage(damage, character.stats.intellect, character.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            drainSP = result.crit ? drainSP * 2 : drainSP;
            target.health.takeDamage(damage);
            target.health.changeCurrentSP(drainSP);
        }
        sendCombatLog(result, target, damage, drainSP);
    }
    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = character.name + " used " + abilityName + " on " + target.name + ". ";
        string log2 = "";
        if (result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            if (result.crit)
            {
                log2 += "Critical Strike! ";

            }
            else
            {
                log2 += target.name + " takes " + damage + " damage and had " + enemiesHit + " SP drained.";
            }
            
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}
