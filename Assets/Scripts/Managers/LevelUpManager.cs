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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
    }

    public int getXPGain()
    {
        int xpGainPercent = Random.Range(minXPGain, maxXPGain);
        int xpGain = Mathf.RoundToInt((highestLevel * xpRequiredPerLevel) * xpGainPercent);
        Debug.Log("XP Gained:" + xpGain);
        return xpGain;
    }

}
