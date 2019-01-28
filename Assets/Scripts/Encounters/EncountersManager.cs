using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountersManager : MonoBehaviour {

    public static EncountersManager instance;
    public GameObject startEncounterPanel;
    public List<GameObject> encounters;
    public EncountersBase currentEncounter;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        startEncounterPanel.SetActive(true);
    }

  

    GameObject getRandomEncounter()
    {
        int r = Random.Range(0, encounters.Count);
        return Instantiate(encounters[r]);
    }

    public void startRandomEncounter()
    {
        startEncounterPanel.SetActive(false);
        GameObject encounter = getRandomEncounter();
        EncountersBase encountersBase = encounter.GetComponent<EncountersBase>();
        currentEncounter = encountersBase;
        currentEncounter.startEncounter();
    }

    public void activateProcedePanel()
    {
        startEncounterPanel.SetActive(true);
    }
}
