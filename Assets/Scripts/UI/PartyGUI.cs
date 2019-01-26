﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyGUI : MonoBehaviour {

    public static PartyGUI instance;
    public GameObject playerDisplayPrefab;
    public Transform playerDisplayContainer;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
    }
    public List<CharacterPlayer> party;
    private void Start()
    {
        StartCoroutine(waitAFrame());
    }

    IEnumerator waitAFrame()
    {
        yield return null;
        for(int i =0; i < party.Count; i++)
        {
            createNewPlayerDisplay(party[i]);
        }
        
    }

    public void createNewPlayerDisplay(CharacterPlayer player)
    {
        GameObject newDisplay = Instantiate(playerDisplayPrefab);
        newDisplay.transform.parent = playerDisplayContainer;
        PlayerDisplayGUI playerDisplay = newDisplay.GetComponent<PlayerDisplayGUI>();
        playerDisplay.initDisplay(player); 
        
    }

}
