using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : CharacterAbility {

	public virtual bool canUseAbility(Character target)
    {
        return true;
    }
}
