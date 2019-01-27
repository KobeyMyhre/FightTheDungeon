using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWave : CharacterAbility
{
    public float healPerIntellect;
    public float damagePerStrength;

    public override string getDescription()
    {
        int heal = getRoundedDamage(healPerIntellect, character.stats.intellect);
        int damage = getRoundedDamage(damagePerStrength, character.stats.strength);

        return "Deals " + damage + " to an enemy. Heals the party for " + heal + ".";
    }

    public override void useAbilty(Character target)
    {
        int heal = getRoundedDamage(healPerIntellect, character.stats.intellect);
        int damage = getRoundedDamage(damagePerStrength, character.stats.strength);
        CombatResults results = target.health.attemptDamage(damage, character.stats.strength, character.stats.critBonusRoll);
        if(!results.miss)
        {
            damage = results.crit ? damage * 2 : damage;
            heal = results.crit ? heal * 2 : heal;
            target.health.takeDamage(damage);
            for(int i =0; i < PartyGUI.instance.party.Count; i++)
            {
                PartyGUI.instance.party[i].health.heal(heal);
            }
        }
        sendCombatLog(results, target, damage, heal);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = character.name + " uses " + abilityName + ". ";
        string log2 = "";
        if (result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            log2 += target.name + " takes " + damage + " damage. Party is healed for " + enemiesHit + ".";
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}
