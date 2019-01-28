using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannabalize : EnemyAbility
{
    public float damagePerEnemyConstition;
    public float healthPercent;
    [Range(0,1)]
    public float needHealThreshold;
    public override void useAbilty(Character target)
    {
        int damage = getRoundedDamage(damagePerEnemyConstition, target.stats.strength);
        int heal = Mathf.RoundToInt(healthPercent * character.health.maxHealth);
        CombatResults result = target.health.attemptDamage(damage, target.stats.strength, character.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            heal = result.crit ? heal * 2 : heal;
            target.health.takeDamage(damage);
            character.health.heal(heal);
        }
        sendCombatLog(result, target, damage, heal);
    }

    public override bool canUseAbility(Character target)
    {
        if(character.health.getCurrentHealthPercent() <= needHealThreshold)
        {
            return base.canUseAbility(target);
        }
        return false;
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " used " + abilityName + " on " + RT.rt_setColor(target.textColor) + target.name + RT.rt_endColor() + ". ";
        string log2 = "";
        if (result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            if (result.crit)
            {
                log2 += RT.rt_setColor(RTColors.purple);
                log2 += "Critical Strike! ";
                log2 += RT.rt_endColor();
            }
            else
            {
                log2 += target.name + " takes " + RT.rt_setColor(RTColors.red) + damage + RT.rt_endColor() + " damage" + " and " + RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " heals for " + RT.rt_setColor(RTColors.green) + enemiesHit + RT.rt_endColor();
            }

        }
        CombatLogger.instance.logCombatString(log1, log2);
    }

}
