using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demoralize : CharacterAbility {

    public int duration;
    public int decreaseAmount;

    public override string getDescription()
    {
        return "Applies a -" + decreaseAmount + " to all enemies strength values for " + duration + " turns.";
    }
    public override string getAttribute()
    {
        return "STR";
    }
    public override void useAbilty(Character target)
    {
        base.useAbilty(target);
        int enemiesHit = 0;
        for (int i = 0; i < TurnManager.instance.enemiesInCombat.Count; i++)
        {
            CombatResults result = TurnManager.instance.enemiesInCombat[i].health.attemptDamage(0, character.stats.strength, character.stats.critBonusRoll);
            if (!result.miss)
            {
                
                TurnManager.instance.enemiesInCombat[i].health.applyEffect(new StatDown(Stat.Str,decreaseAmount,duration));
                enemiesHit++;
            }
        }
        sendCombatLog(null, null, decreaseAmount, enemiesHit);
    }
    public override void sendCombatLog(CombatResults result, Character target, int damage, int enemiesHit = 1)
    {
        string log = RT.rt_setColor(character.textColor) + character.name + RT.rt_endColor() + " reduced the " + Stat.Str + " of " + enemiesHit + " enemies by " + damage;
        CombatLogger.instance.logEffectString(log);
    }
}
