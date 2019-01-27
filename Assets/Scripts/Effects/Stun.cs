using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Effects {

    public override string getLogText()
    {
        return effector.character.name + " is Stunned!";
    }
    public override void onTurnUpdate()
    {
        effector.character.skipTurn = true;
        base.onTurnUpdate();
    }
    public Stun(int _duration)
    {
        duration = _duration;
        displayInText = true;
    }
}
