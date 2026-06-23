# Day 4 Setup Checklist (Shotgun, SMG, Weapon Switching)

## 1) Generate Weapon Assets

1. In Unity top menu click **Game → Create Weapon Assets**.
2. Confirm Assets/Data/Weapons now contains:
   - Pistol.asset
   - Shotgun.asset
   - SMG.asset

## 2) Wire Weapons to Player

1. Select Player in Hierarchy.
2. In Inspector find PlayerShooter.
3. Set Weapons array size to 3.
4. Drag in order:
   - Element 0: Pistol.asset
   - Element 1: Shotgun.asset
   - Element 2: SMG.asset

## 3) Scene Test

1. Press Play.
2. Verify weapon switching:
   - Press 1 → Console logs "Switched to: Pistol"
   - Press 2 → Console logs "Switched to: Shotgun"
   - Press 3 → Console logs "Switched to: SMG"
   - Mouse scroll cycles through weapons
3. Verify firing behavior:
   - Pistol: single accurate ray, click to fire
   - Shotgun: 6 spread rays visible in Scene view, click to fire
   - SMG: rapid single rays, hold to fire

## 4) Day 4 Done Criteria

- All three weapons fire correctly
- Weapon switch works on 1/2/3 and mouse scroll
- Shotgun spread is visible and hits nearby enemies
- SMG fires automatically on hold
- Pistol remains accurate with no spread
- No console errors

## Troubleshooting

If Game menu does not show Create Weapon Assets:
- Confirm Unity has finished compiling (no spinner in bottom right)
- Confirm WeaponFactory.cs is inside Assets/Editor folder

If weapons array is empty after entering Play:
- Confirm all three .asset files are assigned in PlayerShooter before Play

If shotgun feels too weak:
- Open Shotgun.asset and increase damagePerPellet
- Default is 15 per pellet × 6 pellets = 90 max damage per shot

If SMG fires too fast or too slow:
- Open SMG.asset and adjust shotsPerSecond (default 10)
