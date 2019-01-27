using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemy : Character
{
    [Header("Level up vars")]
    [Range(0,3)]
    public float strPerLevel;
    [Range(0, 3)]
    public float aglPerLevel;
    [Range(0, 3)]
    public float conPerLevel;
    [Range(0, 3)]
    public float intPerLevel;
    [Range(0, 3)]
    public float wisPerLevel;
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
        for(int i =0; i < abilities.Count; i++)
        {
            abilities[i].character = this;
        }
        health.init();
        levelEnemy();
    }

    public void levelEnemy()
    {
        int highestLevel = LevelUpManager.instance.highestLevel;
        int level = Random.Range(highestLevel + 2, highestLevel + 4);
        stats.strength += Mathf.RoundToInt(level * strPerLevel);
        stats.agility += Mathf.RoundToInt(level * aglPerLevel);
        stats.constitution += Mathf.RoundToInt(level * conPerLevel);
        stats.intellect += Mathf.RoundToInt(level * intPerLevel);
        stats.wisdom += Mathf.RoundToInt(level * wisPerLevel);
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
