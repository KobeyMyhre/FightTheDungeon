using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityManager : MonoBehaviour {

    public static AbilityManager instance;
    public CharacterPlayer player;
    public GameObject abilityPanel;
    public List<TextMeshProUGUI> abilityNames;
    public TextMeshProUGUI abilityDescription;
    public GameObject descriptionPanel;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        abilityPanel.SetActive(false);
    }

    public void setUpAbilityManager(CharacterPlayer currentPlayer)
    {
        player = currentPlayer;
        for(int i =0; i < abilityNames.Count; i++)
        {
            abilityNames[i].text = currentPlayer.abilities[i].abilityName;
        }
        abilityPanel.SetActive(true);
    }

    public void removeAbilityPanel()
    {
        player = null;
        abilityPanel.SetActive(false);
    }

    public void setAbility(int idx)
    {
        if(player == null) { return; }
        if(player.abilities[idx].hasEnoughSP())
            player.myAbility = player.abilities[idx];
    }

    public void showAbilityDescription(int idx)
    {
        CharacterAbility ability = player.abilities[idx]; 
        abilityDescription.text = ability.abiltyDescription;
        descriptionPanel.SetActive(true);
    }
    public void hideAbilityDescription()
    {
        descriptionPanel.SetActive(false);
    }
}
