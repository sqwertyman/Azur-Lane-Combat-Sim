using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStat
{
    public float baseValue;

    private readonly List<StatModifier> statModifiers;

    public ShipStat(float baseValue)
    {
        this.baseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }
}
