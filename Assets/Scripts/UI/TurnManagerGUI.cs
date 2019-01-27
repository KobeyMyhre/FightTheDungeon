using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnManagerGUI : MonoBehaviour {

    public static TurnManagerGUI instance;
    public TextMeshProUGUI currentCharacter;
    public Transform enemyDisplays;
    public GameObject enemyDisplayPrefab;
    public Image[] turnOrderImages;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }

    public void updateTurnOrder(List<Color> order)
    {
        for(int i =0; i < order.Count;i++)
        {
            turnOrderImages[i].color = order[i];
        }
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
