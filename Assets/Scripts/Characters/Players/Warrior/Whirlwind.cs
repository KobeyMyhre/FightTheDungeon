using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : CharacterAbility {

    public float damagerPerStrength;

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagerPerStrength, character.stats.strength);
        int enemiesHit = 0;
        for (int i = 0; i < TurnManager.instance.enemiesInCombat.Count; i++)
        {
            CombatResults result = TurnManager.instance.enemiesInCombat[i].health.attemptDamage(0, character.stats.intellect, character.stats.critBonusRoll);
            if (!result.miss)
            {
                damage = result.crit ? damage * 2 : damage;
                TurnManager.instance.enemiesInCombat[i].health.takeDamage(damage);
                enemiesHit++;
            }
        }
        sendCombatLog(null, null, damage, enemiesHit);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = character.name + " hit " + enemiesHit + " enemies "+ " for " + damage + " damage";
        CombatLogger.instance.logEffectString(log);
    }
}
