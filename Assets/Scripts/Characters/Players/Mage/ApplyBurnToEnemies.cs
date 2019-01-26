using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBurnToEnemies : CharacterAbility {

    public int burnDamagerPerTurn;
    public int duration;
    public override void useAbilty(Character target)
    {
        for(int i =0; i < TurnManager.instance.enemiesInCombat.Count; i++)
        {
            if (TurnManager.instance.enemiesInCombat[i].health.attemptDamage(0, character.stats.intellect, character.stats.critBonusRoll, character, this, false))
            {
                TurnManager.instance.enemiesInCombat[i].health.applyEffect(new Burn(duration, burnDamagerPerTurn, true));
            }
        }
        CombatLogger.instance.logEffectString("I applied burn to shit");
    }
}
