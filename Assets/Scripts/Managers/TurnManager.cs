using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public static TurnManager instance;
    public Queue<Character> turnOrder;
    public List<Character> enemiesInCombat;
    public List<Character> playersInCombat;

    public Character current;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
    }
    public List<Character> testCombat;
    private void Start()
    {
        initTurnOrder(testCombat);
        startCombatTurns();
    }

    public void initTurnOrder(List<Character> characters)
    {
        turnOrder = new Queue<Character>();
        for(int i =0; i < characters.Count; i++)
        {
            turnOrder.Enqueue(characters[i]);
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
        int enemyCount = 0;
        for(int i =0; i < enemiesInCombat.Count; i++)
        {
            if(enemiesInCombat[i].health.isDead())
            {
                enemyCount++;
            }
        }
        return enemyCount == enemiesInCombat.Count;
    }

    IEnumerator takeTurn()
    {
        yield return null;
        while(!isCombatOver())
        {
            current = turnOrder.Dequeue();
            TurnManagerGUI.instance.setCurrentCharacter(current.name);
            
            
            
            Character target = null;
            CharacterAbility ability = null;
            yield return current.takeTurn(target, ability);
            Debug.Log(current.name + " targeted " + target.name);
            ability.useAbilty(target);
            turnOrder.Enqueue(current);
            yield return null;
        }
        Debug.Log("Combat over");

    }
}
