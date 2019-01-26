using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character {

    [Header("Stuff needed for turn")]
    public CharacterEnemy myTarget;
    public CharacterAbility myAbility;

    public Character grabTarget(TurnManager turnManager)
    {
        Character retval = null;
        retval = turnManager.enemiesInCombat[Random.Range(0, turnManager.enemiesInCombat.Count)];
        return retval;
    }

    public void setTarget(CharacterEnemy enemy)
    {
        myTarget = enemy;
    }
    
    public void setAbility(CharacterAbility ability)
    {
        myAbility = ability;
    }

    public override IEnumerator takeTurn(Character target, CharacterAbility ability)
    {
        myAbility = ability01;
        while(myTarget == null || myAbility == null)
        {
            yield return null;
        }
        target = myTarget;
    }

}
