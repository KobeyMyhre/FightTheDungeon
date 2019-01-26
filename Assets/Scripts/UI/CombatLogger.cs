using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatLogger : MonoBehaviour {
    public static CombatLogger instance;
    public GameObject loggerPanel;
    public TextMeshProUGUI logger;
    public float textDelay;
    public float textDelay2;
    public float textPause;
    WaitForSeconds textWait;
    WaitForSeconds textWait2;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        loggerPanel.SetActive(false);
        textWait = new WaitForSeconds(textDelay);
        textWait2 = new WaitForSeconds(textDelay2);
    }

    bool isDisplayingText;
    public IEnumerator isDisplaying()
    {
        while(isDisplayingText)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        loggerPanel.SetActive(false);
    }

    public void logEffectString(string log)
    {
        loggerPanel.SetActive(true);
        StartCoroutine(displayText(log.ToCharArray()));
    }

    public void logCombatString(Character attacker, Character target, CharacterAbility ability, bool miss, int damage, bool crit)
    {
        string log = attacker.name + "  used " + ability.name + ".";
        string log2 = "";
        if (miss) { log2 += "It Missed..."; }
        else
        {
            if(crit) { log2 += "Critical Hit! "; }
            log2 += target.name + " takes " + damage + " damage!";
        }
        loggerPanel.SetActive(true);
        StartCoroutine(displayText(log.ToCharArray(), log2.ToCharArray()));
    }


    IEnumerator displayText(char[] log)
    {
        isDisplayingText = true;
        logger.text = "";
        int idx = 0;
        while (idx < log.Length)
        {
            logger.text += log[idx];
            idx++;
            yield return textWait;
        }
       
        isDisplayingText = false;
    }
    IEnumerator displayText(char[] log, char[] log2)
    {
        isDisplayingText = true;
        logger.text = "";
        int idx= 0;
        while(idx < log.Length)
        {
            logger.text += log[idx];
            idx++;
            yield return textWait;
        }
        yield return new WaitForSeconds(textPause);
        idx = 0;
        while (idx < log2.Length)
        {
            logger.text += log2[idx];
            idx++;
            yield return textWait2;
        }

        isDisplayingText = false;
    }

}
