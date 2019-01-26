using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{

    public Character character;
    public string abilityName;
    [TextArea]
    public string abiltyDescription;
    public virtual void useAbilty(Character target)
    {

    }

    public virtual string getDescription()
    {
        return abiltyDescription;
    }
}
