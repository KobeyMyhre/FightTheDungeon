using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnEvent();
public class TurnManager : MonoBehaviour {

    public static TurnManager instance;
    public Queue<Character> turnOrder;
    public List<Character> enemiesInCombat;
    public List<Character> playersInCombat;
    public OnEvent onCombatOver;
    public Character current;
    public Character target;
    public CharacterAbility ability;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
    }
    public List<Character> testCombat;
    

    public void initCombat(List<CharacterPlayer> players, List<CharacterEnemy> enemies)
    {
        List<Character> characters = new List<Character>();
        for(int i =0; i < players.Count; i++)
        {
            characters.Add(players[i]);
        }
        for(int i =0; i < enemies.Count; i++)
        {
            characters.Add(enemies[i]);
        }
        initTurnOrder(characters);
        startCombatTurns();
    }

    public void removeCharacterFromCombat(CharacterHealth health)
    {
        if(health.character is CharacterEnemy)
        {
            enemiesInCombat.Remove(health.character);
        }
        if(health.character is CharacterPlayer)
        {
            enemiesInCombat.Remove(health.character);
        }
    }

    public void initTurnOrder(List<Character> characters)
    {
        turnOrder = new Queue<Character>();
        enemiesInCombat.Clear();
        playersInCombat.Clear();
        for(int i =0; i < characters.Count; i++)
        {
            turnOrder.Enqueue(characters[i]);
            characters[i].health.onDeath += removeCharacterFromCombat;
            if(characters[i] is CharacterEnemy)
            {
                enemiesInCombat.Add(characters[i]);
                TurnManagerGUI.instance.addEnemyDisplay((CharacterEnemy)characters[i]);
            }
            if(characters[i] is CharacterPlayer)
            {
                playersInCombat.Add(characters[i]);
            }
        }
    }

    public void startCombatTurns()
    {
        StartCoroutine(takeTurn());
    }
    public CharacterEnemy getCurrentEnemy()
    {
        if(current is CharacterEnemy)
        {
            return (CharacterEnemy)current;
        }
        return null;
    }

    public CharacterPlayer getCurrentPlayer()
    {
        if(current is CharacterPlayer)
        {
            return (CharacterPlayer)current;
        }
        return null;
    }

    bool isCombatOver()
    {
        int playerCount = 0;
        for(int i =0; i < playersInCombat.Count; i++)
        {
            if(playersInCombat[i].health.isDead())
            {
                playerCount++;
            }
        }
        if(playerCount == playersInCombat.Count) { return true; }
        
        return enemiesInCombat.Count == 0;
    }

    IEnumerator takeTurn()
    {
        yield return null;
        while(!isCombatOver())
        {
            current = turnOrder.Dequeue();
            while(current.health.isDead())
            {
                current = turnOrder.Dequeue();
            }
            TurnManagerGUI.instance.setCurrentCharacter(current.name);
            
            
            
            target = null;
            ability = null;
            yield return current.takeTurn(target, ability);
            
            Debug.Log(current.name + " targeted " + target.name);
            ability.useAbilty(target);
            turnOrder.Enqueue(current);
            yield return null;
        }
        Debug.Log("Combat over");
        if(onCombatOver != null)
        {
            onCombatOver();
        }
    }
}
