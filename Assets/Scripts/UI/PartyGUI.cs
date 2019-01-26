using System.Collections;
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
    public CharacterPlayer startPlayer;
    private void Start()
    {
        StartCoroutine(waitAFrame());
    }

    IEnumerator waitAFrame()
    {
        yield return null;
        createNewPlayerDisplay(startPlayer);
    }

    public void createNewPlayerDisplay(CharacterPlayer player)
    {
        GameObject newDisplay = Instantiate(playerDisplayPrefab);
        newDisplay.transform.parent = playerDisplayContainer;
        PlayerDisplayGUI playerDisplay = newDisplay.GetComponent<PlayerDisplayGUI>();
        playerDisplay.initDisplay(player); 
        
    }

}
