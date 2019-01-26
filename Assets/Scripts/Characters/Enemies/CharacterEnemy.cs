using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemy : Character {

	public Character grabTarget(TurnManager turnManager)
    {
        Character retval = null;
        retval = turnManager.playersInCombat[Random.Range(0, turnManager.playersInCombat.Count)];
        return retval;
    }


    public override IEnumerator takeTurn(Character target, CharacterAbility ability)
    {
        yield return new WaitForSeconds(1);
        target = grabTarget(TurnManager.instance);
        ability = ability01;
    }
}
