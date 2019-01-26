using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyDisplayGUI : MonoBehaviour {

    public TextMeshProUGUI name;
    public TextMeshProUGUI hpText;
    public Image hpBar;
    CharacterEnemy myEnemy;
    public void initDisplay(CharacterEnemy enemy)
    {
        myEnemy = enemy;
        name.text = enemy.name;
        updateHealthUI(enemy.health);
        enemy.health.onHealthChange += updateHealthUI;
        enemy.health.onDeath += removeDisplay;
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
