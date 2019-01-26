using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{

    public Character character;
    public string abilityName;
    [TextArea]
    public string abiltyDescription;
    public int spCost;
    public virtual void useAbilty(Character target)
    {
        character.health.changeCurrentSP(spCost);
    }

    public virtual string getDescription()
    {
        return abiltyDescription;
    }
    public bool hasEnoughSP()
    {
        if(character.health.currentSP >= spCost)
        {
            return true;
        }
        return false;
    }
}
