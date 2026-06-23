# Day 1 Setup Checklist (Unity 2022.3.49f1)

Use this to get movement + mouse aim working in SampleScene today.

## Input Choice

For this sprint, use Unity's built-in Input Manager (legacy axes) for fastest implementation and least setup overhead.

Why this choice now:
- No action map authoring needed on day 1
- Works immediately with WASD + mouse in this project
- Easy to migrate to the New Input System later if needed

## Scene Steps

1. Open SampleScene.
2. Add a Directional Light if one does not exist.
3. Create an empty GameObject named GameBootstrap.
4. Add ArenaBootstrapper to GameBootstrap.
5. In the component context menu, click Build Arena.
6. Create a Capsule (or Cube) named Player.
7. Add Rigidbody to Player:
   - Use Gravity: Off
   - Collision Detection: Continuous
8. Add TopDownPlayerController to Player.
9. Ensure Main Camera exists and is tagged MainCamera.
10. Add TopDownCameraFollow to Main Camera.
11. Drag Player into TopDownCameraFollow.target.
12. Position camera roughly at (0, 16, -12).
13. Press Play and test:
   - WASD moves player
   - Mouse position rotates player

## Day 1 Done Criteria

- Player can move in all directions smoothly
- Player faces mouse cursor on arena plane
- Camera follows player smoothly
- No console errors

## Troubleshooting

If player does not rotate:
- Confirm Main Camera has tag MainCamera
- Confirm camera can see arena

If player falls through floor:
- Confirm floor object exists under ArenaRoot and has collider
- Confirm Player has collider and Rigidbody

If movement is too slippery:
- Raise acceleration in TopDownPlayerController

If movement is too slow/fast:
- Adjust moveSpeed in TopDownPlayerController
