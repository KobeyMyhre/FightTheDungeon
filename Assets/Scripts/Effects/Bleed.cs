using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Effects {

    public int damagerPerTurn;

    public override string getLogText()
    {
        return RT.rt_setColor(effector.character.textColor) + effector.character.name + RT.rt_endColor() + " takes " + RT.rt_setColor(RTColors.red) + damagerPerTurn + RT.rt_endColor() + " damage from his bleed";
    }

    public override void onTurnUpdate()
    {
        effector.takeDamage(damagerPerTurn);
        base.onTurnUpdate();
    }

    public Bleed(int _duration, int _damagePerTurn)
    {
        duration = _duration;
        damagerPerTurn = _damagePerTurn;
        displayInText = true;
    }
}
