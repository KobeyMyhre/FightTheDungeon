using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelUpManager : MonoBehaviour {

    public static LevelUpManager instance;
    public int xpIncreasePerLevel;
    public int maxXPGain;
    public int minXPGain;
    public int xpGainDecrease;
    public int highestLevel;
    public int skillPointsPerLevel;
    public int skillPointsRemaining;
    public Stack<CharacterPlayer> needsLevelUp;
    [Header("UI Vars")]
    public GameObject levelUpPanel;
    public CharacterPlayer currentPlayer;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI skillPoints;
    public TextMeshProUGUI strengthVal;
    public TextMeshProUGUI agilityVal;
    public TextMeshProUGUI constututionVal;
    public TextMeshProUGUI intellectVal;
    public TextMeshProUGUI wisdomVal;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        highestLevel = 1;
        needsLevelUp = new Stack<CharacterPlayer>();
        levelUpPanel.SetActive(false);
    }

    public int getMaxXP(int level)
    {
        return (xpIncreasePerLevel * level) + 100;
    }

    public int getXPGain()
    {
        int xpGainCalc = Random.Range(minXPGain, maxXPGain);
        float xpGainPercent = xpGainCalc / 100.0f;
        int xpGain = Mathf.RoundToInt((highestLevel * xpIncreasePerLevel) * xpGainPercent);
        Debug.Log("XP Gained:" + xpGain);
        return xpGain;
    }

    public void levelUpCharacter(CharacterPlayer player)
    {
        if(currentPlayer != null)
        {
            needsLevelUp.Push(player);
            return;
        }
        currentPlayer = player;
        skillPointsRemaining = skillPointsPerLevel;
        initLevelUpPanel();
        if(player.level > highestLevel) { highestLevel = player.level; }
    }

    public void initLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
        charName.text = currentPlayer.name;
        skillPoints.text = skillPointsRemaining.ToString();
        strengthVal.text = currentPlayer.stats.strength.ToString();
        agilityVal.text = currentPlayer.stats.agility.ToString();
        constututionVal.text = currentPlayer.stats.constitution.ToString();
        intellectVal.text = currentPlayer.stats.intellect.ToString();
        wisdomVal.text = currentPlayer.stats.wisdom.ToString();
    }

    public void finishLevelUp()
    {
        levelUpPanel.SetActive(false);
        currentPlayer.health.healToFull();
        currentPlayer.health.spToFull();
        currentPlayer = null;
        if(needsLevelUp.Count > 0)
        {
            levelUpCharacter(needsLevelUp.Pop());
        }
    }

    public void useSkillPoint()
    {
        skillPointsRemaining--;
        skillPoints.text = skillPointsRemaining.ToString();
        if(skillPointsRemaining == 0)
        {
            finishLevelUp();
        }
        
    }

    public void increaseStr()
    {
        currentPlayer.stats.strength++;
        strengthVal.text = currentPlayer.stats.strength.ToString();
        useSkillPoint();
    }
    public void increaseAgl()
    {
        currentPlayer.stats.agility++;
        agilityVal.text = currentPlayer.stats.agility.ToString();
        useSkillPoint();
    }
    public void increaseCon()
    {
        currentPlayer.stats.constitution++;
        currentPlayer.health.updateMaxHealth();
        constututionVal.text = currentPlayer.stats.constitution.ToString();
        useSkillPoint();
    }
    public void increaseInt()
    {
        currentPlayer.stats.intellect++;
        intellectVal.text = currentPlayer.stats.intellect.ToString();
        useSkillPoint();
    }
    public void increaseWis()
    {
        currentPlayer.stats.wisdom++;
        currentPlayer.health.updateMaxSP();
        wisdomVal.text = currentPlayer.stats.wisdom.ToString();
        useSkillPoint();
    }
}
