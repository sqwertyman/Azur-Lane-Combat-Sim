using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//various enums used. order of some important (mainly for armour multipliers)
public enum EquipmentType { DD, CL, CA, BB, Torpedo };
public enum FleetType { Main, Vanguard };
public enum ArmourType { Light, Medium, Heavy };
public enum AmmoType { Normal, HE, AP, Torpedo };

//persistent database of all ships, guns, etc
public static class Database
{
    //[SerializeField]
    //private static Dictionary<int, ShipData> shipList;
    //[SerializeField]
    //private static Dictionary<int, GunData> gunList;
    [SerializeField]
    private static Dictionary<string, ShipData> shipList;
    [SerializeField]
    private static Dictionary<string, WeaponData> gunList;
    [SerializeField]
    private static float[][][] armourMutlipliers;

    //public static Dictionary<int, ShipData> ShipList { get => shipList; set => shipList = value; }
    //public static Dictionary<int, GunData> GunList { get => gunList; set => gunList = value; }
    public static Dictionary<string, ShipData> ShipList { get => shipList; set => shipList = value; }
    public static Dictionary<string, WeaponData> GunList { get => gunList; set => gunList = value; }

    public static void Start()
    {
        //shipList = new Dictionary<int, ShipData>();
        //gunList = new Dictionary<int, GunData>();
        shipList = new Dictionary<string, ShipData>();
        gunList = new Dictionary<string, WeaponData>();
        LoadShips();
        LoadGuns();

        InitArmourMultipliers();
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
        WeaponData[] resources = Resources.LoadAll<WeaponData>(@"Guns");
        foreach (GunData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunList.Add(gun.Name, gun);
        }

        resources = Resources.LoadAll<WeaponData>(@"Torpedoes");
        foreach (TorpedoData gun in resources)
        {
            //gunList.Add(gun.ID, gun);
            gunList.Add(gun.Name, gun);
        }
    }

    //returns the armour damage multiplier for the gun armour and ammo combination
    public static float ArmourMultiplier(EquipmentType gun, ArmourType armour, AmmoType ammo)
    {
        float multiply = armourMutlipliers[(int)gun][(int)armour][(int)ammo];
        return multiply;
    }

    //sets up the armourMultipliers array, with all the different multiplier values. has to follow same orders as appropriate enums
    //order is: [gun class][armour type][ammo type]
    //torps included here for now, may be best to separate at some point
    private static void InitArmourMultipliers()
    {
        //order: light (norm,he,ap), med (norm,he,ap), heavy (norm,he,ap) 

        //dd
        float[][] ddMod = { new float[] { 1f, 1.2f, 0.9f }, new float[] { 0.5f, 0.6f, 0.7f }, new float[] { 0.2f, 0.6f, 0.4f } };
        //cl
        float[][] clMod = { new float[] { 1f, 1.45f, 1f }, new float[] { 0.75f, 1.05f, 0.8f }, new float[] { 0.4f, 0.7f, 0.7f } };
        //ca
        float[][] caMod = { new float[] { 1f, 1.35f, 0.75f }, new float[] { 0.8f, 0.95f, 1.1f }, new float[] { 0.6f, 0.7f, 0.75f } };
        //bb
        float[][] bbMod = { new float[] { 0.7f, 1.4f, 0.3f }, new float[] { 1f, 1.1f, 1.3f }, new float[] { 0.9f, 0.9f, 1f } };
        //torp
        float[][] torpMod = { new float[] { 0f, 0f, 0f, 0.8f }, new float[] { 0f, 0f, 0f, 1f }, new float[] { 0f, 0f, 0f, 1.3f } };

        armourMutlipliers = new float[][][] { ddMod, clMod, caMod, bbMod, torpMod };
    }
}
