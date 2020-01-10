using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Type", menuName = "Ammo")]
public class AmmoData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int projectileSpeed;
    [SerializeField]
    private Color dmgNumberColour;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public Color DmgNumberColour { get => dmgNumberColour; set => dmgNumberColour = value; }
}
