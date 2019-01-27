using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeTwice : CharacterAbility {

    public float damagerPerStrength;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerStrength, character.stats.strength);
        return "Deals " + damage + " damage twice.";
    }


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
        string log = RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " hit " + RT.rt_setColor(target.textColor) + target.name + RT.rt_endColor() + " " + enemiesHit + " times for " + RT.rt_setColor(RTColors.red) + damage + RT.rt_endColor() + " damage";
        CombatLogger.instance.logEffectString(log);
    }
    public override string getAttribute()
    {
        return "STR";
    }
}
