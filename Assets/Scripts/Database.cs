using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//various enums used. order of some important (mainly armour multipliers)
public enum Class { DD, CL, CA, BB, BC, CV };
public enum EquipmentType { DD, CL, CA, BB, Torpedo, TorpedoBomber, Fighter, DiveBomber, AA };
public enum FleetType { Main, Vanguard, Enemy };
public enum ArmourType { Light, Medium, Heavy };
public enum AmmoType { Normal, HE, AP, Torpedo, AirTorpedo };
public enum FiringType { LockOn, Scattershot, Bracketing };

//struct for storing data about a damage instance. used for damage numbers
public struct DamageStruct
{
    public DamageStruct(int _damage, bool _crit)
    {
        damage = _damage;
        crit = _crit;
    }

    public int damage;
    public bool crit;
}

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
    private static float[][][] gunArmourMutlipliers;
    [SerializeField]
    private static float[] torpedoArmourMultipliers;
    [SerializeField]
    private static float[] planeArmourModifiers;

    //public static Dictionary<int, ShipData> ShipList { get => shipList; set => shipList = value; }
    //public static Dictionary<int, GunData> GunList { get => gunList; set => gunList = value; }
    public static Dictionary<string, ShipData> ShipList { get => shipList; set => shipList = value; }
    public static Dictionary<string, WeaponData> GunList { get => gunList; set => gunList = value; }

    public static string savePath, mainFileName, vanguardFileName, enemyFileName;

    public static void Start()
    {
        savePath = Application.persistentDataPath + "/Saves";
        mainFileName = "/mainshipsave";
        vanguardFileName = "/vanguardshipsave";
        enemyFileName = "/enemyshipsave";

        //shipList = new Dictionary<int, ShipData>();
        //gunList = new Dictionary<int, GunData>();
        shipList = new Dictionary<string, ShipData>();
        gunList = new Dictionary<string, WeaponData>();

        LoadAllData();

        InitArmourMultipliers();
    }

    private static void LoadAllData()
    {
        LoadData("Guns", gunList);
        LoadData("Torpedoes", gunList);
        LoadData("Planes", gunList);

        LoadData("Ships", shipList);
        LoadData("Enemies", shipList);
    }

    //generic method to load all T (a databaseitem) into the list from the resource folder directory
    private static void LoadData<T>(string directory, Dictionary<string, T> list)
        where T : DatabaseItem, new()
    {
        T[] tempList = Resources.LoadAll<T>(directory);
        foreach (T item in tempList)
        {
            list.Add(item.Name, item);
        }
    }

    //returns the armour damage multiplier for the gun, armour, and ammo combination
    public static float ArmourMultiplier(EquipmentType gun, ArmourType armour, AmmoType ammo)
    {
        if (gun == EquipmentType.Torpedo)
        {
            return torpedoArmourMultipliers[(int)armour];
        }
        else if (gun == EquipmentType.TorpedoBomber)
        {
            return planeArmourModifiers[(int)armour];
        }
        else
        {
            return gunArmourMutlipliers[(int)gun][(int)armour][(int)ammo];
        }
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
        float[] torpMod = { 0.8f, 1f, 1.3f };
        //air torps
        float[] airTorpMod = { 0.8f, 1.1f, 1.3f };

        gunArmourMutlipliers = new float[][][] { ddMod, clMod, caMod, bbMod };

        torpedoArmourMultipliers = torpMod;

        planeArmourModifiers = airTorpMod;
    }
}
