using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEncounter : EncountersBase {

    public List<CharacterEnemy> enemyPrefabs;
    public int totalEnemies;

    public override void startEncounter()
    {
        List<CharacterEnemy> enemies = new List<CharacterEnemy>();
        for(int i =0; i < enemyPrefabs.Count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[i].gameObject);
            CharacterEnemy enemy = newEnemy.GetComponent<CharacterEnemy>();
            enemy.initEnemy();
            enemies.Add(enemy);
        }
        totalEnemies = enemies.Count;
        TurnManager.instance.onCombatOver += endEncounter;
        TurnManager.instance.initCombat(PartyGUI.instance.party, enemies);
    }

    public override void endEncounter()
    {
        TurnManager.instance.onCombatOver -= endEncounter;
        int xpGainForParty = 0;
        for(int i =0; i < totalEnemies; i++)
        {
            xpGainForParty += LevelUpManager.instance.getXPGain();
        }
        Debug.Log("Total XP Gain: " + xpGainForParty);
        PartyGUI.instance.givePartyXP(xpGainForParty);
        
    }
}
