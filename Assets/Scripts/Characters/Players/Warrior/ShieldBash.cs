using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : CharacterAbility {

    public float damagerPerAgility;
    public int stunDuration;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerAgility, character.stats.agility);
        return "Deals " + damage + " damage to an enemy. Stuns them for " + stunDuration + " turn(s).";
    }

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagerPerAgility, character.stats.agility);
        CombatResults result = target.health.attemptDamage(damage, character.stats.agility, character.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
            target.health.applyEffect(new Stun(stunDuration));
        }
        sendCombatLog(result, target, damage);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = character.name + " used " + abilityName + ".";
        string log2 = "";
        if(result.miss) { log2 += "It Missed..."; }
        else
        {
            if(result.crit) { log2 += "Critical Hit! "; }

            log2 += target.name + " takes " + damage + " damage and is stunned for " + stunDuration + " turns";
            
        }
        CombatLogger.instance.logCombatString(log, log2);
    }
}
