using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerDisplayGUI : MonoBehaviour {

    public TextMeshProUGUI name;
    public TextMeshProUGUI hpVal;
    public Image hpBar;
    public TextMeshProUGUI spVal;
    public Image spBar;
    public CharacterPlayer myPlayer;

    public void initDisplay(CharacterPlayer player)
    {
        myPlayer = player;
        name.text = player.name;
        updateHealthUI(player.health);
        updateSPUI(player.health);
        player.health.onHealthChange += updateHealthUI;
        player.health.onSPChange += updateSPUI;
        player.health.onDeath += removeDisplay;
    }

    public void updateHealthUI(CharacterHealth health)
    {
        hpBar.fillAmount = health.getCurrentHealthPercent();
        hpVal.text = health.currentHealth + "/" + health.maxHealth;
    }
    public void updateSPUI(CharacterHealth health)
    {
        spBar.fillAmount = health.getCurrentSPPercent();
        spVal.text = health.currentSP + "/" + health.maxSP;
    }
    public void removeDisplay(CharacterHealth health)
    {
        myPlayer.health.onHealthChange -= updateHealthUI;
        myPlayer.health.onSPChange -= updateSPUI;
        myPlayer.health.onDeath -= removeDisplay;
        Destroy(gameObject);
    }

    public void displayStats()
    {
        StatsPanelGUI.instance.initStatsPanel(myPlayer.stats);
    }
}
