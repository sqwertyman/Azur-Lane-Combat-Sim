using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : DatabaseItem
{
    [Header("General Equipment Data")]
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, range;
    [SerializeField]
    private AmmoData ammo;

    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public int ProjectileSpeed { get; set; }
    public Sprite Sprite { get; set; }
    public int Range { get => range; set => range = value; }
    public Color DmgNumberColour { get; set; }
    public AmmoData Ammo { get => ammo; set => ammo = value; }

    private void OnEnable()
    {
        Sprite = ammo.Sprite;
        ProjectileSpeed = ammo.ProjectileSpeed;
        DmgNumberColour = ammo.DmgNumberColour;
    }
}
