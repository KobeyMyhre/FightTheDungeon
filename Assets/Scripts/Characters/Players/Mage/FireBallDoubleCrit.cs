using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDoubleCrit : CharacterAbility {

    public float damagerPerIntellect;


    public override string getDescription()
    {
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        return "Deals " + damage + " to an enemy. Crit chance is doubled.";
    }
    public override string getAttribute()
    {
        return "INT";
    }
    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagerPerIntellect, character.stats.intellect);
        int critRoll = target.stats.critBonusRoll;
        if(critRoll == 0) { critRoll = 1; }
        else { critRoll *= 2; }
        Debug.Log("Crit roll used: " + critRoll);
        CombatResults result = target.health.attemptDamage(damage, character.stats.strength, critRoll);
        if (!result.miss)
        {
            damage = result.crit ? damage * 2 : damage;
            target.health.takeDamage(damage);
        }
        sendCombatLog(result, target, damage);
    }
}
