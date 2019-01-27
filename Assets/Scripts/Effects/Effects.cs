using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects
{
    public int duration;
    public CharacterHealth effector;
    public Character applyer;
    public bool displayInText;
    public GameObject display;

    public virtual string getLogText()
    {
        return "Buff does not have log text";
    }

	public virtual bool onApply(CharacterHealth health)
    {
        effector = health;
        if(effector.character is CharacterEnemy)
        {
            CharacterEnemy enemy = (CharacterEnemy)(effector.character);
            display = enemy.myDisplay.displayStatus(this);
        }
        return true;
    }

    public virtual void onTurnUpdate()
    {
        duration--;
        if(duration <= 0)
        {
            onExpire();
        }
    }

    public virtual void onExpire()
    {
        effector.effects.Remove(this);
        StatusSpriteHolder.instance.destroyDisplay(display);
    }
}
