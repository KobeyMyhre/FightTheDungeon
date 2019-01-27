using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Color color;
    public RTColors textColor;
    public string name;
    public CharacterHealth health;
    public CharacterStats stats;
    public CharacterAbility ability01;
    [Header("Level")]
    public int level;

    [HideInInspector]
    public bool skipTurn;
    protected virtual void Awake()
    {
        health = GetComponent<CharacterHealth>();
        health.character = this;
        stats = GetComponent<CharacterStats>();
        stats.character = this;
        ability01.character = this;
    }

    public virtual IEnumerator takeTurn(Character target, CharacterAbility ability)
    {
        yield return null;
    }
    public virtual void setTargetAndAbilty(Character target, CharacterAbility ability)
    {

    }
}
