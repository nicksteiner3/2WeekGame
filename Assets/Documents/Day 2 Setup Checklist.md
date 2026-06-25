# Day 2 Setup Checklist (Shooting + Enemy)

Project: Embervault

Use this right after Day 1 scene setup.

## 1) Player Wiring

1. Select Player.
2. Ensure tag is set to Player.
3. Add PlayerHealth.
4. Add PlayerShooter.
5. Optional: Create a child empty object named Muzzle at front of player and assign it to PlayerShooter.muzzlePoint.

## 2) Enemy Prefab

1. Create Capsule named Enemy.
2. Add Rigidbody:
   - Use Gravity: Off
   - Collision Detection: Continuous
3. Add EnemyChaser.
4. Add EnemyContactDamage.
5. Add EnemyHealth.
6. Make Enemy a prefab in Assets/Prefabs.

## 3) Scene Test

1. Place 2-3 Enemy instances in arena.
2. Press Play.
3. Verify:
   - Left click fires (debug rays visible in Scene/Game while playing)
   - Enemies chase player
   - Enemies deal damage on contact
   - Enemies die when shot repeatedly

## 4) Day 2 Done Criteria

- Shooting works with fire cooldown
- Enemy seek behavior is stable
- Contact damage is applied at interval (not every frame)
- Enemy death works
- No console errors

## Troubleshooting

If enemies do not chase:
- Ensure Player tag is exactly Player
- Ensure Enemy has EnemyChaser and Rigidbody

If shots do not hit enemies:
- Move Muzzle slightly forward from player body
- Ensure Enemy has a collider

If player does not take damage:
- Ensure Player has PlayerHealth
- Ensure Enemy has EnemyContactDamage and collider
