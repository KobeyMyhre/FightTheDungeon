using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character {
    public Color color;
    public int currentXP;
    public int maxXP;

    [Header("Stuff needed for turn")]
    public CharacterEnemy myTarget;
    public CharacterAbility myAbility;

    [Header("Abilities")]
    public CharacterAbility ability02;
    public CharacterAbility ability03;
    public CharacterAbility ability04;
    public CharacterAbility ability05;
    public List<CharacterAbility> abilities;
    public OnEvent onCombatOver;
    protected override void Awake()
    {
        base.Awake();
        abilities = new List<CharacterAbility>();
        abilities.Add(ability01);
        abilities.Add(ability02);
        abilities.Add(ability03);
        abilities.Add(ability04);
        abilities.Add(ability05);
        for(int i =0; i < abilities.Count; i++)
        {
            abilities[i].character = this;
        }
    }
    private void Start()
    {
        maxXP = LevelUpManager.instance.getMaxXP(0);
        onCombatOver += health.spToFull;
        onCombatOver += health.healToFull;
        onCombatOver += health.clearStatuses;
        health.onDeath += removeFromParty;
    }

    

    public void combatCleanUp()
    {
        if(onCombatOver != null)
            onCombatOver();
        TurnManager.instance.onCombatOver -= combatCleanUp;
    }

    public void gainXP(int xp)
    {
        currentXP += xp;
        if(currentXP >= maxXP)
        {
            currentXP -= maxXP;
            maxXP += LevelUpManager.instance.xpIncreasePerLevel;
            Debug.Log("Level Up");
            level++;
            LevelUpManager.instance.levelUpCharacter(this);
        }
    }

    public void removeFromParty(CharacterHealth health)
    {
        PartyGUI.instance.party.Remove(this);
    }

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
        AbilityManager.instance.setUpAbilityManager(this);
        myAbility = null;
        myTarget = null;

       // myAbility = ability01;
        
        while (myTarget == null || myAbility == null)
        {
            yield return null;
        }
        TurnManager.instance.ability = myAbility;
        TurnManager.instance.target = myTarget;
        AbilityManager.instance.removeAbilityPanel();
    }

    public override void setTargetAndAbilty(Character target, CharacterAbility ability)
    {
        target = myTarget;
        ability = myAbility;
    }
}
