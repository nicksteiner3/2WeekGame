using UnityEditor;
using UnityEngine;

public static class WeaponFactory
{
    [MenuItem("Game/Create Weapon Assets")]
    public static void CreateAllWeapons()
    {
        CreateWeapon("Pistol",
            weaponIndex: 0,
            damagePerPellet: 25f,
            pelletsPerShot: 1,
            shotsPerSecond: 4f,
            maxDistance: 40f,
            spreadAngle: 0f,
            isAutomatic: false);

        CreateWeapon("Shotgun",
            weaponIndex: 1,
            damagePerPellet: 15f,
            pelletsPerShot: 6,
            shotsPerSecond: 1.5f,
            maxDistance: 18f,
            spreadAngle: 18f,
            isAutomatic: false);

        CreateWeapon("SMG",
            weaponIndex: 2,
            damagePerPellet: 12f,
            pelletsPerShot: 1,
            shotsPerSecond: 10f,
            maxDistance: 32f,
            spreadAngle: 5f,
            isAutomatic: true);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("[WeaponFactory] Pistol, Shotgun, and SMG assets created in Assets/Data/Weapons.");
    }

    private static void CreateWeapon(string weaponName, int weaponIndex, float damagePerPellet,
        int pelletsPerShot, float shotsPerSecond, float maxDistance, float spreadAngle, bool isAutomatic)
    {
        string folder = "Assets/Data/Weapons";

        if (!AssetDatabase.IsValidFolder("Assets/Data"))
        {
            AssetDatabase.CreateFolder("Assets", "Data");
        }

        if (!AssetDatabase.IsValidFolder(folder))
        {
            AssetDatabase.CreateFolder("Assets/Data", "Weapons");
        }

        string path = $"{folder}/{weaponName}.asset";

        if (AssetDatabase.LoadAssetAtPath<WeaponData>(path) != null)
        {
            Debug.Log($"[WeaponFactory] {weaponName}.asset already exists, skipping.");
            return;
        }

        WeaponData data = ScriptableObject.CreateInstance<WeaponData>();
        data.weaponName = weaponName;
        data.weaponIndex = weaponIndex;
        data.damagePerPellet = damagePerPellet;
        data.pelletsPerShot = pelletsPerShot;
        data.shotsPerSecond = shotsPerSecond;
        data.maxDistance = maxDistance;
        data.spreadAngleDegrees = spreadAngle;
        data.isAutomatic = isAutomatic;

        AssetDatabase.CreateAsset(data, path);
    }
}
