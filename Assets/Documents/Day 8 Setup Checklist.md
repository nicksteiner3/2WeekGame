# Day 8 Setup Checklist (Upgrade Screen - 6 Upgrades)

Project: Embervault

## 1) Create UpgradeManager GameObject

1. In Hierarchy, right-click → Create Empty. Name it UpgradeManager.
2. Add Component → UpgradeManager script.

## 2) Create UpgradeData Assets

Create 6 ScriptableObject assets in Assets/Data/Upgrades. For each, right-click → Create → Embervault → Upgrade Data:

1. **Damage.asset**
   - Upgrade Name: "Damage"
   - Description: "Increase weapon damage per pellet. +20% per level"
   - Current Level: 0

2. **FireRate.asset**
   - Upgrade Name: "Fire Rate"
   - Description: "Shoot faster. +15% speed per level"
   - Current Level: 0

3. **MoveSpeed.asset**
   - Upgrade Name: "Move Speed"
   - Description: "Move faster around the arena. +10% speed per level"
   - Current Level: 0

4. **DashCharge.asset**
   - Upgrade Name: "Dash Charge"
   - Description: "Unlock and improve dash ability (coming soon). +20% per level"
   - Current Level: 0

5. **ExtractionBonus.asset**
   - Upgrade Name: "Extraction Bonus"
   - Description: "Earn more currency on extraction. +10% reward per level"
   - Current Level: 0

6. **PickupRadius.asset**
   - Upgrade Name: "Pickup Radius"
   - Description: "Pick up currency from farther away (coming soon). +10% per level"
   - Current Level: 0

(Note: Cost per level is fixed and automatically set: 100, 175, 275, 400, 550)

## 3) Wire UpgradeManager

1. Select UpgradeManager in Hierarchy.
2. In Inspector, expand "Upgrades" array to size 6.
3. Drag the 6 UpgradeData assets into slots 0-5 in order:
   - [0] Damage.asset
   - [1] FireRate.asset
   - [2] MoveSpeed.asset
   - [3] DashCharge.asset
   - [4] ExtractionBonus.asset
   - [5] PickupRadius.asset

## 4) Create Upgrade Screen UI

### 4a) Create Canvas
1. Right-click in Hierarchy → UI → Canvas. Name it UpgradeScreenCanvas.
2. Set Canvas Scaler to "Scale With Screen Size" (1920x1080 ref).
3. Set Canvas to "Screen Space - Overlay" or "Screen Space - Camera".

### 4b) Create UI Structure

**Inside UpgradeScreenCanvas:**

1. Create a **Panel** named "Header"
   - Anchor top-center
   - Position (0, -30)
   - Size (900, 80)
   - Text child (TextMeshPro): "UPGRADES"
     - Font size 48
   - Text child (TextMeshPro): "Total Currency: 0"
     - Name: CurrencyText
     - Font size 32
     - Position (0, -40)

2. Create a UI object named "UpgradeGrid" and add a **Grid Layout Group** component
   - Anchor center
   - Position (0, -100)
   - Size (1000, 600)
   - In Inspector click Add Component → Layout → Grid Layout Group
   - Grid Layout Group: Columns 3, Child size (280, 180)
   - Spacing (10, 10)

3. Inside UpgradeGrid, create 6 **Buttons** named "UpgradePanel_0" through "UpgradePanel_5":
    - Each button panel should have:
     - **UpgradeName** (TextMeshPro, font size 28, top)
     - **LevelText** (TextMeshPro, font size 20, "Level X/5")
     - **CostText** (TextMeshPro, font size 24, "Cost: 100")
     - **DescriptionText** (TextMeshPro, font size 16, description text, wrapped)
       - **BuyButton** (Button, text "BUY")

### 4c) Add UpgradePanelUI to Each Panel

For each of the 6 upgrade panels (UpgradePanel_0 through UpgradePanel_5):

1. Add Component → UpgradePanelUI
2. Set Upgrade Type dropdown:
   - UpgradePanel_0: Damage
   - UpgradePanel_1: FireRate
   - UpgradePanel_2: MoveSpeed
   - UpgradePanel_3: DashCharge
   - UpgradePanel_4: ExtractionBonus
   - UpgradePanel_5: PickupRadius
3. Drag the corresponding TextMeshPro texts into fields:
   - Upgrade Name → UpgradeName
   - Description → DescriptionText
   - Level Text → LevelText
   - Cost Text → CostText
   - Buy Button → BuyButton
4. Leave Parent Screen and upgrade type parent fields empty (will auto-find)

### 4d) Add UpgradeScreen Component

1. Select UpgradeScreenCanvas.
2. Add Component → UpgradeScreen script.
3. Drag UpgradeGrid into "Upgrade Grid Container" field.
4. Drag CurrencyText into "Total Currency Text" field.
5. Drag a Button (e.g., bottom-right Exit button) into "Exit Button" field.
6. CanvasGroup will be auto-found.
7. Optional (recommended): disable UpgradeScreenCanvas in Hierarchy at edit time. It will be shown programmatically after Run Summary.

## 5) Wire RunSummaryScreen to UpgradeScreen

1. Select RunSummaryScreenCanvas.
2. In RunSummaryScreen component, drag UpgradeScreenCanvas into "Upgrade Screen" field.

## 6) Verify Scene Setup

Ensure your scene has:
- ✓ CurrencyManager
- ✓ UpgradeManager (with 6 upgrades wired)
- ✓ UpgradeScreenCanvas (with UI hierarchy and UpgradeScreen component)
- ✓ RunSummaryScreenCanvas
- ✓ All player scripts updated with upgrade integration

## 7) Scene Test

1. Press Play.
2. Survive for 3 seconds, enter extraction zone, wait 5 seconds.
3. Extraction completes → Run Summary Screen appears.
4. Press SPACE → Upgrade Screen appears.
5. Verify:
   - All 6 upgrades visible with correct names and descriptions
   - Level displays "Level 0/5" for all
   - Costs show: 100, 175, 275, 400, 550 (then MAX)
   - Total Currency shows correct amount (e.g., 110 if you earned 75+35 from 0s35m)
   - Click Buy on any upgrade → currency deducts, level increments
   - Reload to verify upgrades persist in UpgradeManager instance
   - Move Speed, Fire Rate, Damage changes are immediately applied in-game
   - Press ESC or Exit button → game resumes

## 8) Day 8 Done Criteria

- 6 upgrades with 5 levels each
- Fixed cost curve: 100, 175, 275, 400, 550
- Buy button deducts currency and increments level
- Level display updates immediately
- Damage upgrade applies multiplier (×1.0 → ×2.0)
- Fire Rate upgrade applies multiplier (×1.0 → ×1.75)
- Move Speed upgrade applies multiplier (×1.0 → ×1.5)
- Extraction Bonus, Dash Charge, Pickup Radius display-only (functional in later days)
- Upgrade Screen pauses time while open
- ESC or Exit dismisses screen and resumes game
- No console errors
- Currency persists, upgrades persist in instance (not yet saved to disk; that's Day 10)

## Troubleshooting

If upgrades don't appear:
- Confirm UpgradeManager has all 6 UpgradeData assets assigned
- Confirm UpgradeScreenCanvas has UpgradeScreen component
- Check console for "[UpgradeManager] Initialized" message

If Buy button doesn't work:
- Confirm UpgradePanelUI is on each panel
- Confirm Upgrade Type is set correctly for each panel
- Confirm Parent Screen is assigned (or will auto-find)

If upgrades don't apply:
- Confirm UpgradeManager.Instance exists (should be visible in Hierarchy)
- Check fire rate: press mouse button rapidly, should be faster at level 5
- Check move speed: press WASD, should move faster at level 5
- Check damage: look at enemy damage numbers in console when they die

If currency display is wrong:
- Confirm CurrencyManager still has correct total
- Confirm UpgradeScreen's CurrencyText field is assigned
