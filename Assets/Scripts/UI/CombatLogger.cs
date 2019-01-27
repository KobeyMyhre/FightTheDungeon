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

    public void logCombatString(string log1, string log2)
    {
        
        loggerPanel.SetActive(true);
        StartCoroutine(displayText(log1.ToCharArray(), log2.ToCharArray()));
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
            if(Input.GetMouseButton(0))
            {
                logger.text = new string(log);
                break;
            }
            yield return textWait;
        }
       
        isDisplayingText = false;
    }
    IEnumerator displayText(char[] log, char[] log2)
    {
        isDisplayingText = true;
        logger.text = "";
        int idx= 0;
        bool input = false;
        while(idx < log.Length)
        {
            logger.text += log[idx];
            idx++;
            if (Input.GetMouseButton(0))
            {
                logger.text = new string(log);
                logger.text += new string(log2);
                break;
            }
            yield return textWait;
        }
        if(!input)
        {
            yield return new WaitForSeconds(textPause);
            idx = 0;
            while (idx < log2.Length)
            {
                logger.text += log2[idx];
                idx++;
                if (Input.GetMouseButton(0))
                {
                    logger.text = new string(log);
                    logger.text += new string(log2);
                    break;
                }
                yield return textWait2;
            }
        }
        

        isDisplayingText = false;
    }

}
