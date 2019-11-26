using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : DatabaseItem
{
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo;

    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
}
