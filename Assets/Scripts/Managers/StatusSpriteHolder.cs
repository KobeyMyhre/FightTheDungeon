using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSpriteHolder : MonoBehaviour {

    public static StatusSpriteHolder instance;
    public List<Sprite> statusSprites;
    public GameObject statusPrefab;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        
    }

    public Sprite getSprite(Effects effects)
    {
        if(effects is Burn)
        {
            return statusSprites[0];
        }
        if(effects is Bleed)
        {
            return statusSprites[1];
        }
        if(effects is StatDown)
        {
            return statusSprites[2];
        }
        if(effects is Stun)
        {
            return statusSprites[3];
        }
        return null;
    }
    public void destroyDisplay(GameObject display)
    {
        Destroy(display);
    }
    public GameObject addStatusDisplay(Effects effects)
    {
        GameObject newStatus = Instantiate(statusPrefab);
        newStatus.GetComponent<Image>().sprite = getSprite(effects);
        return newStatus;
    }
}
