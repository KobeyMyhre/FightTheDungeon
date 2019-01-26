using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour {

    public static LevelUpManager instance;
    public int xpIncreasePerLevel;
    public int maxXPGain;
    public int minXPGain;
    public int xpGainDecrease;
    public int highestLevel;
    public int xpRequiredPerLevel;
    public GameObject levelUpPanel;
    public CharacterPlayer currentPlayer;
    public int skillPointsPerLevel;
    public int skillPointsRemaining;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        highestLevel = 1;
    }

    public int getXPGain()
    {
        int xpGainCalc = Random.Range(minXPGain, maxXPGain);
        float xpGainPercent = xpGainCalc / 100.0f;
        int xpGain = Mathf.RoundToInt((highestLevel * xpRequiredPerLevel) * xpGainPercent);
        Debug.Log("XP Gained:" + xpGain);
        return xpGain;
    }

    public void levelUpCharacter(CharacterPlayer player)
    {
        levelUpPanel.SetActive(true);
        skillPointsRemaining = skillPointsPerLevel;

    }

    public void useSkillPoint()
    {
        skillPointsRemaining--;
        if(skillPointsRemaining == 0)
        {
            levelUpPanel.SetActive(false);
        }
        currentPlayer.health.healToFull();
        currentPlayer.health.spToFull();
        currentPlayer = null;
    }

    public void increaseStr()
    {
        currentPlayer.stats.strength++;
    }
    public void increaseAgl()
    {
        currentPlayer.stats.agility++;
    }
    public void increaseCon()
    {
        currentPlayer.stats.constitution++;
        currentPlayer.health.updateMaxHealth();
    }
    public void increaseInt()
    {
        currentPlayer.stats.intellect++;
    }
    public void increaseWis()
    {
        currentPlayer.stats.wisdom++;
        currentPlayer.health.updateMaxSP();
    }
}
