using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : CharacterAbility
{
    
    public int damagerPerStrength;
    public override void useAbilty(Character target)
    {
        int damage = character.stats.strength * damagerPerStrength;
        target.health.attemptDamage(damage, character.stats.strength, target.stats.critBonusRoll);
    }
}
