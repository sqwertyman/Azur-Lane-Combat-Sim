using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plane", menuName = "Plane")]
public class PlaneWeaponData : WeaponData
{
    [Header("Plane Specific Data")]
    [SerializeField]
    private Sprite planeSprite;
    [SerializeField]
    private int noOfProj;
    [SerializeField]
    private int speed;

    public int Speed { get => speed; set => speed = value; }
    public int NoOfProj { get => noOfProj; set => noOfProj = value; }
    public Sprite PlaneSprite { get => planeSprite; set => planeSprite = value; }
}
