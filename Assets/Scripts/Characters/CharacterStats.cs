using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    Str,
    Agl,
    Con,
    Int,
    Wis
}


public class CharacterStats : MonoBehaviour
{
    public Character character;
    [Header("Crit stuff")]
    public int critBonusRoll = 0;
    [Header("Strength")]
    public int strength;
    [Header("Agility")]
    public int agility;
    [Header("Constitution")]
    public int constitution;
    public int healthPerConstitution;
    [Header("Intellect")]
    public int intellect;
    [Header("Wilpower")]
    public int wisdom;
    public int spPerWisdom;
	

    public int getState(Stat stat)
    {
        switch(stat)
        {
            case Stat.Str:
                return strength;
            case Stat.Agl:
                return agility;
            case Stat.Con:
                return constitution;
            case Stat.Int:
                return intellect;
            case Stat.Wis:
                return wisdom;
        }
        return 0;
    }

}
