using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEStun : CharacterAbility {

    public float damagePerIntellect;
    public int duration;

    public override string getDescription()
    {
        int damage = getRoundedDamage(damagePerIntellect, character.stats.intellect);
        return "Deals " + damage + " to all enemies and stuns them for " + duration + " turn(s).";
    }

    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(damagePerIntellect, character.stats.intellect);
        int enemeisHit = 0;
        for(int i = 0; i < TurnManager.instance.enemiesInCombat.Count; i++)
        {
            CombatResults result = TurnManager.instance.enemiesInCombat[i].health.attemptDamage(damage, character.stats.intellect, character.stats.critBonusRoll);
            if(!result.miss)
            {
                damage = result.crit ? damage * 2 : damage;
                TurnManager.instance.enemiesInCombat[i].health.applyEffect(new Stun(duration));
                TurnManager.instance.enemiesInCombat[i].health.takeDamage(damage);
                enemeisHit++;
            }
        }
        sendCombatLog(null, null, damage, enemeisHit);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = character.name + " hit " + enemiesHit + " enemies for " + damage + " damage" + " and stuns them for 1 turn";
        CombatLogger.instance.logEffectString(log);
    }
}
