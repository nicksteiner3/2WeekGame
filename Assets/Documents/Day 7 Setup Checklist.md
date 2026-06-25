# Day 7 Setup Checklist (Currency Reward & Run Summary)

Project: Embervault

## 1) Create CurrencyManager GameObject

1. In Hierarchy, right-click → Create Empty. Name it CurrencyManager.
2. Add Component → CurrencyManager script.
3. Leave all default settings (Instance will auto-init).

## 2) Create Run Summary Screen

### 2a) Create Canvas for Summary
1. Right-click in Hierarchy → UI → Canvas. Name it RunSummaryScreenCanvas.
2. Set Canvas → Canvas Scaler to "Scale With Screen Size" (1920x1080 ref).
3. Set Canvas to "Screen Space - Overlay" or "Screen Space - Camera" (your choice).

### 2b) Add TextMeshPro Text Elements

Inside RunSummaryScreenCanvas, create three TextMeshPro text objects:

1. **SurvivalTimeText**
   - Anchor top-center
   - Position: (0, -100)
   - Font size: 40
   - Text: "Survival Time: 0:00" (placeholder)

2. **RunCurrencyText**
   - Anchor center
   - Position: (0, 0)
   - Font size: 32
   - Text: "Run Reward: +0" (placeholder)

3. **TotalCurrencyText**
   - Anchor center
   - Position: (0, -60)
   - Font size: 32
   - Text: "Total Currency: 0" (placeholder)

4. **ContinuePromptText**
   - Anchor bottom-center
   - Position: (0, 50)
   - Font size: 28
   - Text: "Press SPACE to continue" (placeholder)

### 2c) Add RunSummaryScreen Component
1. Select RunSummaryScreenCanvas.
2. Add Component → RunSummaryScreen script.
3. Drag each text object into the corresponding fields:
   - Survival Time Text → SurvivalTimeText
   - Run Currency Text → RunCurrencyText
   - Total Currency Text → TotalCurrencyText
   - Continue Prompt Text → ContinuePromptText
4. Canvas Group will be auto-found.

## 3) Wire RunManager

1. Select RunManager in Hierarchy.
2. In Inspector, drag RunSummaryScreenCanvas into the "Run Summary Screen" field.

## 4) Wire ExtractionZone

1. Select ExtractionZone (or its prefab) in Hierarchy.
2. In Inspector, confirm or adjust:
   - Base Currency Reward: 75
   - Currency Per Minute: 15
   - (These match the Locked V1 spec)

## 5) Verify Scene Setup

Ensure your scene has:
- ✓ RunManager
- ✓ CurrencyManager
- ✓ ExtractionZone (with materials)
- ✓ RunSummaryScreenCanvas (hidden initially)
- ✓ Player + EnemySpawner

## 6) Scene Test

1. Press Play.
2. Let the game run for ~3 seconds.
3. Extraction zone appears. Walk into it.
4. After 5 seconds of standing in the zone, extraction completes.
5. Verify:
   - Console shows "Added X currency" message
   - Run Summary Screen appears and is visible
   - Survival time is displayed correctly (should be ~8 seconds or so)
   - Run Reward shows the calculated amount (base 75 + bonus)
   - Total Currency is displayed
   - Game is paused (enemies don't move)
   - Pressing SPACE dismisses the screen and resumes game

## 7) Day 7 Done Criteria

- CurrencyManager tracks total currency across runs
- Extraction zone calculates reward: 75 + (15 × minutes survived)
- Run Summary Screen displays all required info
- Summary screen pauses time while shown
- SPACE key dismisses screen and resumes game
- No console errors
- Currency persists in CurrencyManager instance

## Troubleshooting

If currency is not awarded:
- Confirm CurrencyManager is in the scene
- Confirm RunManager has RunSummaryScreen reference assigned
- Check console for "[CurrencyManager] Added X currency" message

If Run Summary Screen doesn't appear:
- Confirm RunSummaryScreenCanvas has RunSummaryScreen component
- Confirm all text fields are assigned in RunSummaryScreen Inspector
- Check console for "[RunSummaryScreen] Shown" message
- Verify Canvas is set to active initially (can be set inactive)

If currency calculation is wrong:
- Check ExtractionZone Inspector for Base Currency Reward (75) and Currency Per Minute (15)
- Verify run time is correct (should match Survival Time shown on screen)

If SPACE doesn't dismiss screen:
- Confirm Time.timeScale is actually set to 0 when screen shows (check Console)
- Ensure keyboard input is not being intercepted elsewhere
