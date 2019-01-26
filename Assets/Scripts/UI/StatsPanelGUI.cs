using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsPanelGUI : MonoBehaviour {

    public static StatsPanelGUI instance;
    public TextMeshProUGUI name;
    public TextMeshProUGUI strVal;
    public TextMeshProUGUI aglVal;
    public TextMeshProUGUI conVal;
    public TextMeshProUGUI intVal;
    public TextMeshProUGUI wisVal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else { Destroy(this); }
    }

    public void initStatsPanel(CharacterStats stats)
    {
        name.text = stats.character.name;
        strVal.text = stats.strength.ToString();
        aglVal.text = stats.agility.ToString();
        conVal.text = stats.constitution.ToString();
        intVal.text = stats.intellect.ToString();
        wisVal.text = stats.wisdom.ToString();
    }
}
