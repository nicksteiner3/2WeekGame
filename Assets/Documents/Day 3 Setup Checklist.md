# Day 3 Setup Checklist (Spawner + Run Management)

Project: Embervault

Use this to wire up waves and run flow.

## 1) Make Enemy Prefab

1. Create Capsule in scene named Enemy.
2. Configure it exactly as Day 2:
   - Rigidbody (Gravity: Off, Collision: Continuous)
   - EnemyChaser
   - EnemyContactDamage
   - EnemyHealth
3. Drag it into Assets/Prefabs and name it Enemy.prefab.
4. Delete from scene.

## 2) Add Spawner to Scene

1. Create empty GameObject named EnemySpawner.
2. Add EnemySpawner component.
3. Drag Enemy.prefab into EnemySpawner.enemyPrefab.
4. Adjust if needed:
   - spawnRadius: controls ring distance (default 12 is good)
   - baseSpawnRate: enemies/sec at start (default 1.0)
   - intensityIncreaseInterval: 30s is per spec
   - spawnRateIncreasePerInterval: default 0.5 is reasonable

## 3) Add Run Manager

1. Create empty GameObject named RunManager.
2. Add RunManager component.
3. Optionally drag Player into playerHealth field (auto-finds if empty).
4. Optionally drag EnemySpawner into enemySpawner field (auto-finds if empty).

## 4) Scene Test

1. Press Play.
2. Verify:
   - Enemies spawn around arena perimeter
   - Spawn rate increases every 30 seconds (check Console)
   - Enemies cap at max (you can see "max reached" behavior)
   - When you die, Console shows run end time
   - No console errors

## 5) Day 3 Done Criteria

- Enemies spawn in waves
- Spawn intensity ramps every 30s
- Enemy count stays under cap
- Player death ends run and logs time
- Run can restart cleanly
- No console errors

## Troubleshooting

If enemies do not spawn:
- Confirm EnemySpawner.enemyPrefab is assigned
- Confirm Player exists in scene
- Check Console for errors

If spawn rate does not increase:
- Confirm intensityIncreaseInterval is > 0
- Check Console logs at 30s mark

If run does not end on death:
- Confirm RunManager is in scene
- Confirm PlayerHealth is wired or auto-found
