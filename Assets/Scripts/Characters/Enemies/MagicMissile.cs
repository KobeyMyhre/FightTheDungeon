using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : CharacterAbility {

    public float damagerPerIntellect;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        return "Deals " + damage + " damage.";
    }

    public override void useAbilty(Character target)
    {
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        CombatResults result = target.health.attemptDamage(damage, character.stats.intellect, target.stats.critBonusRoll);
        if (!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
        }
        sendCombatLog(result, target, damage);
    }
}
