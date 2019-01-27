using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedStrike : EnemyAbility {
    
    public float damagePerStrength;
    public float bleedDamagePerStrength;
    public int bleedDuration;
    public override void useAbilty(Character target)
    {
        int bleedDamage = getRoundedDamage(bleedDamagePerStrength, character.stats.strength);
        int damage = getRoundedDamage(damagePerStrength, character.stats.strength);
        CombatResults result = target.health.attemptDamage(damage, character.stats.strength, character.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
            target.health.applyEffect(new Bleed(bleedDuration, bleedDamage));
        }
        sendCombatLog(result, target, damage);
    }

    
    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " used " + abilityName + " on " + RT.rt_setColor(target.textColor) + target.name + RT.rt_endColor() + ".";
        string log2 = "";
        if(result.miss)
        {
            log2 += " It Misses...";
        }
        else
        {
            if(result.crit)
            {
                log2 += RT.rt_setColor(RTColors.purple);
                log2 += "Critical Strike! ";
                log2 += RT.rt_endColor();
            }
            else
            {
                log2 += target.name + " takes " + RT.rt_setColor(RTColors.red) + damage + RT.rt_endColor() + " damage" + " and starts bleeding";
            }
            
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}
