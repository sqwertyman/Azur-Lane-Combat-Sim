using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : DatabaseItem
{
    [Header("Basic Equipment Data")]
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, antiAir;

    public EquipmentType Type { get => type; set => type = value; }
    public int AntiAir { get => antiAir; set => antiAir = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
}
