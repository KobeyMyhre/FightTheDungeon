using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnManagerGUI : MonoBehaviour {

    public static TurnManagerGUI instance;
    public Text currentCharacter;
    public Transform enemyDisplays;
    public GameObject enemyDisplayPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }

    public void setCurrentCharacter(string name)
    {
        currentCharacter.text = name;
    }

    public void addEnemyDisplay(CharacterEnemy enemy)
    {
        GameObject newDisplay = Instantiate(enemyDisplayPrefab);
        newDisplay.transform.parent = enemyDisplays;
        EnemyDisplayGUI displayGUI = newDisplay.GetComponent<EnemyDisplayGUI>();
        displayGUI.initDisplay(enemy);
    }
}
