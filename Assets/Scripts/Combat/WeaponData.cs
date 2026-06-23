using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public string weaponName = "Pistol";
    public int weaponIndex = 0;

    [Header("Firing")]
    public float damagePerPellet = 25f;
    public int pelletsPerShot = 1;
    public float shotsPerSecond = 4f;
    public float maxDistance = 40f;
    public float spreadAngleDegrees = 0f;
    public bool isAutomatic = false;
}
