﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resonance : CharacterAbility {

    public float damagerPerCombinedStat;
    public int agiltyBoost;
    public int duration;
    public override string getDescription()
    {
        int roll = Mathf.RoundToInt((character.stats.intellect + character.stats.strength) / 2.0f);
        int damage = getRoundedDamage(damagerPerCombinedStat, roll);
        return "Deals " + damage + " damage to an enemy. Boosts party Agility by " + agiltyBoost + " for " + duration + " turns.";
    }

    public override string getAttribute()
    {
        return "STR/INT";
    }

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int roll = Mathf.RoundToInt((character.stats.intellect + character.stats.strength) / 2.0f);
        int damage = getRoundedDamage(damagerPerCombinedStat, roll);
        int newDuration = duration;
        CombatResults result = target.health.attemptDamage(damage, roll, character.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            newDuration = result.crit ? duration * 2 : duration;
            target.health.takeDamage(damage);
            for(int i =0; i < PartyGUI.instance.party.Count; i++)
            {
                PartyGUI.instance.party[i].health.applyEffect(new StatDown(Stat.Agl, -agiltyBoost, newDuration));
            }
        }
        sendCombatLog(result, target, damage, newDuration);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " uses " + abilityName + ". ";
        string log2 = "";
        if (result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            log2 += RT.rt_setColor(target.textColor) + target.name + RT.rt_endColor() + " takes " + RT.rt_setColor(RTColors.red) + damage + RT.rt_endColor() + " damage. Party Agility is boosted by " + agiltyBoost + " for " + enemiesHit + " turns";
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}
