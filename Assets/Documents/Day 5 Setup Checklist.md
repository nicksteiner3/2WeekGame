# Day 5 Setup Checklist (Magazine, Reload, Ammo HUD)

## 1) Update Weapon Assets

Open each weapon asset in Assets/Data/Weapons and confirm or set ammo values:

- Pistol.asset: Magazine Size = 12, Reload Time = 1.0
- Shotgun.asset: Magazine Size = 6, Reload Time = 1.5
- SMG.asset: Magazine Size = 30, Reload Time = 1.8

## 2) Create HUD Canvas

1. In Hierarchy right-click → UI → Canvas. Name it HUD.
2. Configure Canvas Scaler:
   - Select the HUD Canvas in the Hierarchy.
   - In the Inspector, find the "Canvas Scaler" component.
   - Set "UI Scale Mode" to "Scale With Screen Size".
   - Set "Reference Resolution" to X: 1920, Y: 1080.
3. Inside HUD Canvas, create two UI → Text (TextMeshPro) objects:
   - Name first: AmmoText. Anchor bottom-right. Position roughly (-160, 80).
   - Name second: HealthText. Anchor bottom-left. Position roughly (160, 80).
4. Set font size around 28 for both.

## 3) Add HUDController

1. Create empty GameObject named HUDManager.
2. Add HUDController component.
3. Drag AmmoText into the Ammo Text field.
4. Drag HealthText into the Health Text field.
5. Leave Player Shooter and Player Health empty (auto-found).

## 4) Scene Test

1. Press Play.
2. Verify:
   - Bottom-right shows weapon name and ammo count (e.g. "Pistol\n12 / 12")
   - Firing decrements ammo count
   - Running out of ammo triggers auto-reload
   - R key manually triggers reload
   - "RELOADING..." text appears during reload
   - Ammo refills when reload completes
   - Bottom-left shows HP value
   - Switching weapons shows correct ammo for each weapon independently

## 5) Day 5 Done Criteria

- Magazine depletes on firing
- Auto-reload triggers on empty mag
- R key triggers manual reload
- Each weapon tracks ammo independently
- HUD shows current ammo and mag size
- HUD shows HP value
- No console errors

## Troubleshooting

If ammo text does not appear:
- Confirm Canvas is in World Space or Screen Space Overlay
- Confirm TextMeshPro package is installed (Window → Package Manager)
- Confirm AmmoText is assigned in HUDController

If reload does not trigger:
- Confirm weapon assets have Magazine Size > 0
- Confirm Reload Time > 0

If ammo does not reset between weapons:
- Each weapon tracks independently — switch to Pistol, fire 3 shots, switch to SMG, Pistol still shows 9 on return
