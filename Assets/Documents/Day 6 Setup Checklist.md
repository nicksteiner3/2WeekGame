# Day 6 Setup Checklist (Extraction Zone - 3:00 Mark)

Project: Embervault

## 1) Create ExtractionZone Prefab

1. In Hierarchy, right-click → 3D Object → Cylinder. Name it ExtractionZone.
2. Scale it to a large flat disc:
   - Scale X: 4, Y: 0.1, Z: 4 (large, flat platform)
3. Position it somewhere in the arena floor, e.g., (0, 0.05, 0).
4. Add a Collider:
   - Add Component → Collider → Sphere Collider
   - Set "Is Trigger" to ON
   - Set Radius to 3 (covers the visual area)
5. Add the ExtractionZone script:
   - Add Component → ExtractionZone
   - Channel Duration: 5
   - Leave materials empty for now (optional visual polish)
6. Create a material for the zone visual:
   - Right-click in Assets/Data → Material. Name it ExtractionZoneMaterial.
   - Set color to bright cyan (e.g., (0, 1, 1, 0.5)) for visibility
   - Drag material onto the cylinder in the Hierarchy
7. Drag the ExtractionZone GameObject into Assets/Prefabs and name it ExtractionZone.prefab.

## 2) Wire RunManager

1. Select RunManager in Hierarchy.
2. In the Inspector, drag the ExtractionZone (from Hierarchy) into the "Extraction Zone" field.
3. Verify Extraction Spawn Time is set to 3 (for testing; change to 180 for final 3-minute build).

## 3) Verify PlayerHealth Event

1. Open PlayerHealth.cs and confirm it has:
   - `public event HealthEvent OnDeath;`
   - `OnDeath?.Invoke();` inside the `if (IsDead)` block
2. (This was already done in the script update.)

## 4) Scene Test

1. Press Play and let the game run.
2. After ~2-3 seconds, watch for the extraction zone to appear.
3. Verify:
   - Extraction zone visual appears at the 3-second mark
   - Walking into the zone triggers extraction channel
   - After 5 seconds in the zone, run ends and extraction completes
   - If you take damage while extracting, the channel continues (doesn't reset)
   - If you die while extracting, the extraction is cancelled and run ends

## 5) Day 6 Done Criteria

- Extraction zone spawns at 3:00 mark
- 5-second channel when player is inside zone
- Run ends on successful extraction (console logs "Extraction completed!")
- Run ends if player dies (extraction is cancelled)
- Zone visual is clearly visible and positioned well
- No console errors

## Troubleshooting

If extraction zone doesn't appear:
- Confirm RunManager has ExtractionZone reference assigned
- Confirm ExtractionZone script is on the prefab
- Check console for "Extraction zone activated at 3 seconds!" message

If extraction completes immediately:
- Confirm Channel Duration is set to 5 (in ExtractionZone Inspector)
- Confirm Player tag is set correctly on Player GameObject

If player can't enter zone:
- Confirm Sphere Collider "Is Trigger" is ON
- Confirm radius is large enough (default 3 should work)
- Test with Console: `Physics.OverlapSphere()` to debug collision
