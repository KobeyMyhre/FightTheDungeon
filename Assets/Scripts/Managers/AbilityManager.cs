using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AbilityManager : MonoBehaviour {

    public static AbilityManager instance;
    public CharacterPlayer player;
    public GameObject abilityPanel;
    public List<TextMeshProUGUI> abilityNames;
    public List<Image> abilityImage;
    public TextMeshProUGUI abilityDescription;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityCost;
    public TextMeshProUGUI attribute;
    public Image descriptionPanel;
    public Image lastPressed;
    public Color selectedColor;
    Color lastPressedColor;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        abilityPanel.SetActive(false);
        descriptionPanel.gameObject.SetActive(false);
    }

    public void setSelectedButtonColor(Image button)
    {
        if(lastPressed != null)
        {
            lastPressed.color = lastPressedColor;
        }
        if(button == null) { return; }
        lastPressedColor = button.color;
        button.color = selectedColor;
        lastPressed = button;
    }

    public void setUpAbilityManager(CharacterPlayer currentPlayer)
    {
        player = currentPlayer;
        setSelectedButtonColor(null);
        for (int i =0; i < abilityNames.Count; i++)
        {
            abilityNames[i].text = currentPlayer.abilities[i].abilityName;
            
        }
        for(int i =0; i < abilityImage.Count; i++)
        {
            abilityImage[i].color = currentPlayer.color;
        }
        descriptionPanel.color = currentPlayer.color;
        abilityPanel.SetActive(true);
        
    }

    public void removeAbilityPanel()
    {
        player = null;
        lastPressed = null;
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
        abilityDescription.text = ability.getDescription();
        abilityName.text = ability.abilityName;
        abilityCost.text = ability.spCost.ToString();
        abilityCost.color = ability.hasEnoughSP() ? Color.white : Color.red;
        attribute.text = ability.getAttribute();
        descriptionPanel.gameObject.SetActive(true);
    }
    public void hideAbilityDescription()
    {
        descriptionPanel.gameObject.SetActive(false);
    }
}
