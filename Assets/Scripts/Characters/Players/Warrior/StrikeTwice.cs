using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeTwice : CharacterAbility {

    public float damagerPerStrength;

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagerPerStrength, character.stats.strength);
        int timesHit = 0;
        int totalDamage = 0;
        CombatResults results = target.health.attemptDamage(damage, character.stats.strength, character.stats.critBonusRoll);
        if(!results.miss)
        {
            int usedDamage = results.crit ? damage * 2 : damage;
            target.health.takeDamage(usedDamage);
            timesHit++;
            totalDamage += usedDamage;
        }
        results = target.health.attemptDamage(damage, character.stats.strength, character.stats.critBonusRoll);
        if (!results.miss)
        {
            int usedDamage = results.crit ? damage * 2 : damage;
            target.health.takeDamage(usedDamage);
            timesHit++;
            totalDamage += usedDamage;
        }
        sendCombatLog(results, target, totalDamage, timesHit);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = character.name + " hit " + target.name + " " + enemiesHit + " times for " + damage + "damage";
        CombatLogger.instance.logEffectString(log);
    }

}
