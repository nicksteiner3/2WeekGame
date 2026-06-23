# Extraction Micro-Survivor: Locked V1 Spec (June 22, 2026)

This document locks scope for a 2-week Unity sprint with a realistic solo schedule.

## 1) Locked Product Decisions

- Platform: Windows only
- Camera and controls: Top-down, WASD movement, mouse aim
- Distribution target: Steam build-ready in about 2 weeks
- Art: Placeholder-only for v1
- Audio: No owned assets yet (plan includes sourcing options)
- Difficulty target: Medium
- Performance target: Keep reasonable and stable, no strict hard benchmark for v1

## 2) Combat and Player Systems

### Movement
- WASD movement with acceleration smoothing
- Mouse-aimed firing

### Dash (kept and defined)
- Input: Space
- Distance: 4.5 units
- Cooldown: 3.5 seconds
- Charges: 1 base charge
- Recharge: Regain 1 charge on cooldown complete
- Invulnerability: None (no i-frames in v1)
- Cancel rules: Dash locks movement input for dash duration, then returns control

### Weapons and Ammo
- Weapon set: Pistol, Shotgun, SMG
- Ammo model: Magazine + reload for all weapons
- Weapon switch: Number keys or mouse wheel

Suggested starting values (tune later):
- Pistol: 12 mag, 1.0s reload
- Shotgun: 6 mag, 1.5s reload
- SMG: 30 mag, 1.8s reload

## 3) Run Structure and Rewards

- Extraction appears at 3:00
- Extraction channel is 5 seconds
- Taking damage during channel resets the extraction timer
- Currency reward only on successful extraction
- No stay-longer bonus after extraction appears

## 4) Progression Economy (Locked)

Goal: Full upgrade completion around 20 hours of successful play.

Note: 20h is aggressive but workable if extraction success rate is not too low. Keep this target for now and tune after 5-10 playtests.

### Upgrade Rules
- 6 upgrades total
- Each upgrade max level: 5
- Total upgrade levels to buy: 30

Upgrades:
- Damage
- Fire Rate
- Move Speed
- Dash Charge
- Extraction Bonus
- Pickup Radius

### Cost Curve per level (same for each upgrade)
- Level 1: 100
- Level 2: 175
- Level 3: 275
- Level 4: 400
- Level 5: 550

Total per upgrade: 1500
Total for all 6 upgrades: 9000

### Reward Baseline
- Successful extraction reward: 75 + (15 x minutes survived rounded down)
- At a standard 3-5 minute run, expected reward about 120-150

This puts full completion near the requested target if average run time and extraction success remain moderate.

## 5) Persistence Approach

Chosen approach: JSON save file (best balance of speed and future-proofing).

Why JSON over PlayerPrefs:
- Easier to inspect and debug during development
- Easier to version and migrate later
- Better structure for progression data

Save payload:
- totalCurrency
- unlockedUpgradeLevels (6 values)
- settings (volume, sensitivity placeholder)
- saveVersion

Write timing:
- On successful extraction
- On upgrade purchase
- On application quit

## 6) Audio Plan (No Owned Assets Yet)

Use temporary, license-safe audio now and replace later if needed.

Recommended sources:
- Kenney (CC0-style game asset packs)
- OpenGameArt (verify per-file license)
- Freesound (only use sounds with clear commercial-use license)

Rules for Steam safety:
- Keep a small credits and licenses file in project docs
- Store source URL and license for every imported audio file
- Avoid assets with attribution requirements you cannot satisfy

Minimum v1 audio set:
- 3 weapon shots (pistol, shotgun, smg)
- 1 enemy death
- 1 player hit
- 1 reload
- 1 extraction success
- 1 menu click
- 1 looping background track

## 7) Realistic 14-Day Schedule (1-2 h/day)

This schedule assumes about 20 total focused hours and prioritizes shipping over polish.

Day 1
- Project setup and scene bootstrap
- Player move + mouse aim

Day 2
- Basic shooting (single weapon)
- Enemy with contact damage

Day 3
- Enemy spawner with simple intensity ramp
- Death/respawn flow

Day 4
- Add Shotgun and SMG
- Weapon switch input

Day 5
- Magazine + reload implementation
- Ammo UI on HUD

Day 6
- Extraction zone spawn at 3:00
- 5s channel and reset-on-damage behavior

Day 7
- Currency on successful extraction
- Run summary screen

Day 8
- Upgrade screen
- Implement 6 upgrades with 5 levels each

Day 9
- Dash implementation (spec above)
- Dash charge upgrade integration

Day 10
- JSON save/load for progression
- Save on extraction and purchases

Day 11
- Main menu + pause menu wiring
- Basic game state transitions

Day 12
- Integrate placeholder SFX and one music loop
- Basic audio mixer levels

Day 13
- Bug fixing pass
- Input edge cases, extraction edge cases, reload bugs

Day 14
- Build candidate for Windows
- Smoke test
- Prepare Steam build checklist

## 8) Steam Readiness Checklist (V1)

- Windows build runs cleanly from fresh launch
- No blocker bugs in core loop
- Upgrade save persists across sessions
- Store capsule art and screenshots prepared
- Short description written
- Basic trailer capture optional (can be post-build)

## 9) Scope Guardrails (Do Not Add in V1)

- No new enemy families
- No boss fight
- No procedural generation
- No inventory or crafting
- No multiplayer
- No cosmetic systems

If a new idea appears during sprint, log it in a post-launch ideas list and continue current plan.
