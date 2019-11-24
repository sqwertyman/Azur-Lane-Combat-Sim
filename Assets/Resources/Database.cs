﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    [SerializeField]
    private static Dictionary<int, ShipData> shipList;
    //[SerializeField]
    //private static Dictionary<int, GunData> gunList;
    [SerializeField]
    private static Dictionary<string, ShipData> shipNamesList;
    [SerializeField]
    private static Dictionary<string, EquipmentData> gunNamesList;

    public static Dictionary<int, ShipData> ShipList { get => shipList; set => shipList = value; }
    //public static Dictionary<int, GunData> GunList { get => gunList; set => gunList = value; }
    public static Dictionary<string, ShipData> ShipNamesList { get => shipNamesList; set => shipNamesList = value; }
    public static Dictionary<string, EquipmentData> GunNamesList { get => gunNamesList; set => gunNamesList = value; }

    public static void Start()
    {
        shipList = new Dictionary<int, ShipData>();
        //gunList = new Dictionary<int, GunData>();
        shipNamesList = new Dictionary<string, ShipData>();
        gunNamesList = new Dictionary<string, EquipmentData>();
        LoadShips();
        LoadGuns();
    }

    //use generics?
    private static void LoadShips()
    {
        ShipData[] resources = Resources.LoadAll<ShipData>(@"Ships");
        foreach (ShipData ship in resources)
        {
            shipList.Add(ship.ID, ship);
            shipNamesList.Add(ship.Name, ship);
        }
    }

    private static void LoadGuns()
    {
        EquipmentData[] resources = Resources.LoadAll<EquipmentData>(@"Guns");
        foreach (GunData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunNamesList.Add(gun.Name, gun);
        }

        resources = Resources.LoadAll<EquipmentData>(@"Torpedoes");
        foreach (TorpedoData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunNamesList.Add(gun.Name, gun);
        }
    }
}
