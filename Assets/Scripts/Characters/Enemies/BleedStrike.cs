﻿using System.Collections;
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
        string log1 = character.name + " used " + abilityName + " on " + target.name + ".";
        string log2 = "";
        if(result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            if(result.crit)
            {
                log2 += "Critical Strike! ";

            }
            else
            {
                log2 += target.name + " takes " + damage + " damage" + " and starts bleeding";
            }
            
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}