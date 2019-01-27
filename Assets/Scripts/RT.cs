using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RTColors
{
    black,
    blue,
    green,
    orange,
    purple,
    red,
    white,
    yellow
}


public static class RT
{
    public static string rt_endColor()
    {
        return "</color>";
    }
    public static string rt_setColor(RTColors colors)
    {
        return "<color="+ colors.ToString() + ">";
    }


}
