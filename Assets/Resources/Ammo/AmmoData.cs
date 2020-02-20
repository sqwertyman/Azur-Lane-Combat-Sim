using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Type", menuName = "Ammo")]
public class AmmoData : ScriptableObject
{
    [SerializeField]
    private AmmoType ammo;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int projectileSpeed;
    [SerializeField]
    private int splashRange;
    [SerializeField]
    private Color dmgNumberColour;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public Color DmgNumberColour { get => dmgNumberColour; set => dmgNumberColour = value; }
    public AmmoType Ammo { get => ammo; set => ammo = value; }
    public int SplashRange { get => splashRange; set => splashRange = value; }
}
