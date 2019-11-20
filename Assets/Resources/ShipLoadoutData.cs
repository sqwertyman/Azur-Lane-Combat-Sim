using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Loadout", menuName = "Ship Loadout")]

public class ShipLoadoutData : ScriptableObject
{
    [SerializeField]
    private ShipData ship;
    [SerializeField]
    private GunData mainGun, auxGun;

    public ShipData Ship { get => ship; set => ship = value; }
    public GunData MainGun { get => mainGun; set => mainGun = value; }
    public GunData AuxGun { get => auxGun; set => auxGun = value; }
}
