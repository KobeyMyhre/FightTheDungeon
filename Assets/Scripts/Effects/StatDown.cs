using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDown : Effects
{
    public int decreaseAmount;
    public Stat stat;
    public override bool onApply(CharacterHealth health)
    {
        base.onApply(health);
        modify(stat, health.character.stats, -decreaseAmount);
        return true;
    }
    public override void onExpire()
    {
        modify(stat, effector.character.stats, decreaseAmount);
        base.onExpire();
    }

    public StatDown(Stat _stat, int _decreaseAmount, int _duration)
    {
        duration = _duration;
        decreaseAmount = _decreaseAmount;
        stat = _stat;
    }

    public void modify(Stat stat, CharacterStats charStat, int amount)
    {
        switch (stat)
        {
            case Stat.Str:
                charStat.strength += amount;
                break;
            case Stat.Agl:
                charStat.agility += amount;
                break;
            case Stat.Con:
                charStat.constitution += amount;
                charStat.character.health.updateMaxHealth();
                break;
            case Stat.Int:
                charStat.intellect += amount;
                break;
            case Stat.Wis:
                charStat.wisdom += amount;
                charStat.character.health.updateMaxSP();
                break;
        }
    }

}
