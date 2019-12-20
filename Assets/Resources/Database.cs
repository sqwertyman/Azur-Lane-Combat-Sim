using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { CL, DD, BB, Torpedo };
public enum FleetType { Main, Vanguard };

public static class Database
{
    //[SerializeField]
    //private static Dictionary<int, ShipData> shipList;
    //[SerializeField]
    //private static Dictionary<int, GunData> gunList;
    [SerializeField]
    private static Dictionary<string, ShipData> shipList;
    [SerializeField]
    private static Dictionary<string, EquipmentData> gunList;

    //public static Dictionary<int, ShipData> ShipList { get => shipList; set => shipList = value; }
    //public static Dictionary<int, GunData> GunList { get => gunList; set => gunList = value; }
    public static Dictionary<string, ShipData> ShipList { get => shipList; set => shipList = value; }
    public static Dictionary<string, EquipmentData> GunList { get => gunList; set => gunList = value; }

    public static void Start()
    {
        //shipList = new Dictionary<int, ShipData>();
        //gunList = new Dictionary<int, GunData>();
        shipList = new Dictionary<string, ShipData>();
        gunList = new Dictionary<string, EquipmentData>();
        LoadShips();
        LoadGuns();
    }

    //use generics?
    private static void LoadShips()
    {
        ShipData[] resources = Resources.LoadAll<ShipData>(@"Ships");
        foreach (ShipData ship in resources)
        {
            //shipList.Add(ship.ID, ship);
            shipList.Add(ship.Name, ship);
        }
    }

    private static void LoadGuns()
    {
        EquipmentData[] resources = Resources.LoadAll<EquipmentData>(@"Guns");
        foreach (GunData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunList.Add(gun.Name, gun);
        }

        resources = Resources.LoadAll<EquipmentData>(@"Torpedoes");
        foreach (TorpedoData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunList.Add(gun.Name, gun);
        }
    }
}
