using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : CharacterAbility
{
    
    public float damagerPerStrength;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerStrength, character.stats.strength);
        return "Deals " + damage + " damage.";
    }

    public override string getAttribute()
    {
        return "STR";
    }

    public override void useAbilty(Character target)
    {
        int damage = getRoundedDamage(damagerPerStrength, character.stats.strength);
        CombatResults result = target.health.attemptDamage(damage, character.stats.strength, target.stats.critBonusRoll);
        if(!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
        }
        sendCombatLog(result, target, damage);
    }
}
