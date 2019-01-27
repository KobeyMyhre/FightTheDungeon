using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBurnToEnemies : CharacterAbility {

    public float burnDamagerPerIntellect;
    public int duration;

    public override string getDescription()
    {
        int damage = getRoundedDamage(burnDamagerPerIntellect, character.stats.intellect);
        return "Applies a burn effect to all enemies, that deals " + damage + " damage every turn for " + duration + "turns.";
    }
    public override string getAttribute()
    {
        return "INT";
    }
    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int damage = getRoundedDamage(burnDamagerPerIntellect, character.stats.intellect);
        int burnsApplied = 0;
        for(int i =0; i < TurnManager.instance.enemiesInCombat.Count; i++)
        {
            CombatResults result = TurnManager.instance.enemiesInCombat[i].health.attemptDamage(0, character.stats.intellect, character.stats.critBonusRoll);
            if(!result.miss)    
            {
                damage = result.crit ? damage * 2 : damage;
                TurnManager.instance.enemiesInCombat[i].health.applyEffect(new Burn(duration, damage, true));
                burnsApplied++;
            }
        }
        sendCombatLog(null, null,0, burnsApplied);
    }

    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = "";
        log += RT.rt_setColor(character.textColor)+ character.name +RT.rt_endColor() + " applied Burn to " + enemiesHit;
        log += enemiesHit > 1 ? " enemies" : " enemy";
        if(enemiesHit == 0)
        {
            log = abilityName + " Misses...";
        }
        CombatLogger.instance.logEffectString(log);
    }
}
