using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Effects
{
    public int damagerPerTurn;

    public override string getLogText()
    {
        return effector.character.name + " takes " + damagerPerTurn + " damage from his burns";
    }

    public override void onTurnUpdate()
    {
        effector.takeDamage(damagerPerTurn);
        base.onTurnUpdate();
    }

    public Burn(int _duration, int _damagerPerTurn, bool _display)
    {
        duration = _duration;
        damagerPerTurn = _damagerPerTurn;
        displayInText = _display;
    }
}
