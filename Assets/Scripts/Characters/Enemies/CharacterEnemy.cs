using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemy : Character
{
    public List<EnemyAbility> abilities;
	public Character grabTarget(TurnManager turnManager)
    {
        Character retval = null;
        retval = turnManager.playersInCombat[Random.Range(0, turnManager.playersInCombat.Count)];
        return retval;
    }


    public void initEnemy()
    {
        Awake();
        health.init();
    }

    protected virtual CharacterAbility getAbilty(Character target)
    {
        List<CharacterAbility> retval = new List<CharacterAbility>();
        for(int i =0; i < abilities.Count; i++)
        {
            if(abilities[i].canUseAbility(target))
            {
                retval.Add(abilities[i]);
            }
        }
        int r = Random.Range(0, retval.Count);
        if(r < retval.Count)
            return retval[r];
        return ability01;
    }

    public override IEnumerator takeTurn(Character target, CharacterAbility ability)
    {
        yield return new WaitForSeconds(1);
        TurnManager.instance.target = grabTarget(TurnManager.instance);
        TurnManager.instance.ability = getAbilty(target);

    }
    public override void setTargetAndAbilty(Character target, CharacterAbility ability)
    {
        
    }
}
