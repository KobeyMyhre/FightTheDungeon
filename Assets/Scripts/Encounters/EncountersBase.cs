using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountersBase : MonoBehaviour {

	public virtual void startEncounter()
    {

    }

    public virtual void endEncounter()
    {
        EncountersManager.instance.activateProcedePanel();
    }
}
