using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : CharacterAbility {
    public int coolDown;
    public int currentCD;
    public virtual bool canUseAbility(Character target)
    {
        currentCD--;
        if (currentCD <= 0)
        {
            currentCD = coolDown;
            return true;
        }
        return false;
    }
}
