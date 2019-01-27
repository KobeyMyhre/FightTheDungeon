using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : CharacterAbility {

    public float damagerPerCombined;

    public override string getDescription()
    {
        int roll = Mathf.RoundToInt((character.stats.intellect + character.stats.strength) / 2.0f);
        int damage = getRoundedDamage(damagerPerCombined, roll);
        return "Deals " + damage + " damage.";
    }

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int roll = Mathf.RoundToInt((character.stats.intellect + character.stats.strength) / 2.0f);
        int damage = getRoundedDamage(damagerPerCombined, roll);
        CombatResults result = target.health.attemptDamage(damage, character.stats.strength, target.stats.critBonusRoll);
        if (!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
        }
        sendCombatLog(result, target, damage);
    }
}
