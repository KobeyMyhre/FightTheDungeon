using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasis : CharacterAbility {
    public int stunDuration;
    
    public override string getDescription()
    {
        return "Stuns the target for " + stunDuration + " turns.";
    }

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int roll = Mathf.RoundToInt((character.stats.intellect + character.stats.strength) / 2.0f);
        CombatResults result = target.health.attemptDamage(0, roll, character.stats.critBonusRoll);
        if(!result.miss)
        {
            target.health.applyEffect(new Stun(stunDuration));
        }
        sendCombatLog(result, target, stunDuration);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log1 = character.name + " uses " + abilityName + " on " + RT.rt_setColor(target.textColor) + target.name + RT.rt_endColor() + ".";
        string log2 = "";
        if (result.miss)
        {
            log2 += "It Misses...";
        }
        else
        {
            log2 += target.name + " is stunned for " + damage + " turns";
        }
        CombatLogger.instance.logCombatString(log1, log2);
    }
}
