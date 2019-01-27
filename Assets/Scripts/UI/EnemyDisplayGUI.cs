using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyDisplayGUI : MonoBehaviour {

    public TextMeshProUGUI name;
    public TextMeshProUGUI hpText;
    public Image hpBar;
    public Transform statusBar;
    CharacterEnemy myEnemy;

    public void showStats()
    {
        StatsPanelGUI.instance.showStatsPanel(myEnemy.stats);
    }
    public void showOriginal()
    {
        StatsPanelGUI.instance.showOriginal();
    }
    
    public GameObject displayStatus(Effects effect)
    {
        GameObject statusDisplay = StatusSpriteHolder.instance.addStatusDisplay(effect);
        statusDisplay.transform.parent = statusBar;
        return statusDisplay;
    }

    public void initDisplay(CharacterEnemy enemy)
    {
        myEnemy = enemy;
        name.text = enemy.name;
        updateHealthUI(enemy.health);
        enemy.health.onHealthChange += updateHealthUI;
        enemy.health.onDeath += removeDisplay;
        enemy.myDisplay = this;
    }

    public void updateHealthUI(CharacterHealth health)
    {
        hpBar.fillAmount = health.getCurrentHealthPercent();
        hpText.text = health.currentHealth + "/" + health.maxHealth;
    }

    public void assignPlayerTarget()
    {
        CharacterPlayer player = TurnManager.instance.getCurrentPlayer();
        if(player != null)
        {
            if(player.myAbility != null)
            {
                player.setTarget(myEnemy);
            }
        }
    }

    public void removeDisplay(CharacterHealth health)
    {
        myEnemy.health.onHealthChange -= updateHealthUI;
        myEnemy.health.onDeath -= removeDisplay;
        Destroy(gameObject);
    }
}
