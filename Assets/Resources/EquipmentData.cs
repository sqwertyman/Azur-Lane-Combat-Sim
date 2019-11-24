using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : DatabaseItem
{
    [SerializeField]
    private EquipmentType type;
    public EquipmentType Type { get => type; set => type = value; }
}
