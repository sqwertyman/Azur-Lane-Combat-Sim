using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseItem : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private int id;

    public int ID { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
}
