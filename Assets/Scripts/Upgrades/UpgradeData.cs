using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeData", menuName = "Embervault/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea(2, 4)]
    public string description;
    
    [SerializeField] private int[] costPerLevel = { 100, 175, 275, 400, 550 };
    public int currentLevel;

    public int MaxLevel => 5;

    public int GetCostForNextLevel()
    {
        if (currentLevel >= MaxLevel)
        {
            return 0; // Already maxed
        }
        return costPerLevel[currentLevel];
    }

    public int GetTotalCostToMaxFromZero()
    {
        int total = 0;
        for (int i = 0; i < MaxLevel; i++)
        {
            total += costPerLevel[i];
        }
        return total;
    }

    public void PurchaseLevel()
    {
        if (currentLevel < MaxLevel)
        {
            currentLevel++;
            Debug.Log($"[UpgradeData] {upgradeName} upgraded to level {currentLevel}.");
        }
    }

    public void ResetLevel()
    {
        currentLevel = 0;
    }
}
