## Prefabs

The project contains prefabs for:


## Scenes

### Main

Basic UI for main menu with transitions to level menu and survival mode. The application can be closed via this scene.

### LevelMenu

Basic UI for level menu with two levels and a transition to a main menu.

### Level Scenes

Each level consists of:

Level is finished when player reaches the `EndSign`, which also fires transition to Jump Animation.

### Survival

Survival Mode. This level has no ending except dying. After death, a UI PopUp is shown with the player's achieved score and player can decide if he wants to try again or quit to main menu.

The principle of creating the world is very similar as in basic levels except the enemies and math billboards are generated each `VerticalStep` with the help of `Equation Oracle`.

#### Equation Oracle

`Equation Oracle` chooses the operation of math billboard and the number of enemies spawned after this billboard. It generates random equations until the maximum of player units after colliding with billboard is bigger than 2. The EquationOracle computes number of units in next enemy squad  as a random value in range `[_successPercentage * maxPlayerUnits, maxPlayerUnits)` so that the number of player units after colliding with enemies is at least one when he choose the sign with better equation.

## Classes

