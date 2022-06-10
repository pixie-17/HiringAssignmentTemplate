## Prefabs

The project contains prefabs for:
* `EndSign` - billboard without equation, displays the end of level
* `Enemy` - main unit for enemy with movement and Canvas, which shows the current count of units in formation
* `EnemyPoint` - spawn positions for enemies with possibilities of changing enemy colors, prefabs, count and angle
* `EnemyUnit` - dummy unit for enemy with movement
* `Leader` - main unit for player with movement and Canvas, which shows the current count of units in formation
* `Unit` - dummy unit for player with movement
* `SpawnPosition` - relative positions for formation
* `Sign` - basic math billboard. It is possible to change the color and equation from the Inspector for versatility.
* `SignPair` - pair or two math bilboards for quick instantiation. Each of the sign from the pair has a reference to the other sign for quick access. 
* `OrangeSign` - orange variation of `Sign` 
* `RedSign` - red variation of `Sign` 
* `Tile` - copy of a 1x1 tile from assets


## Scenes

### Main

Basic UI for main menu with transitions to level menu and survival mode. The application can be closed via this scene.

### LevelMenu

Basic UI for level menu with two levels and a transition to a main menu.

### Level Scenes

Each level consists of:

* Spawn points for player
* `FloorManager` - the tile prefab and tile material can be changed via Inspector for versatility
* Placed `SignPairs` and `EndSign`
* Placed `EnemyPoints`
* basic `UI` - pause button, pause menu, level failed menu and level finished menu
* `GameManager` - main component of level. The speed of player units and enemy units can be changed via Inspector. This component contains a `SquadManager` for the player which recomputes the formation on player's units. The prefabs for a leader and basic units and their colors can be changed via Inspector.

Level is finished when player reaches the `EndSign`, which also fires transition to Jump Animation.
### Survival

Survival Mode. This level has no ending except dying. After death, a UI PopUp is shown with the player's achieved score and player can decide if he wants to try again or quit to main menu.

The principle of creating the world is very similar as in basic levels except the enemies and math billboards are generated each `signStep` seconds with the help of `Equation Oracle`.

#### Equation Oracle

`Equation Oracle` chooses the operation of math billboard and the number of enemies spawned after this billboard. It generates random equations until the maximum of player units after colliding with billboard is bigger than 2. When it finds suitable equation, it generates enemy squad with count in `[0.65f * maxPlayerUnits, maxPlayerUnits - 1]` so that the number of player units after colliding with enemies is at least one when he choose the sign with better equation.

## Classes

Documentation for more 'complex' classes:

* `EnemyMovement` -  the initial state of enemy is Idle and it moves according to the position of player, but only if the player is in certain radius (3f), where the animation of running is fired. It also detects collisions with player units and computes the resulting squad count for player and enemy and according to the results decides if the level failed or it can continue.
* `GameManager` - it hold references to `FloorManager`, `SurvivalManager`, `SquadManager` for player and game object for player's leader for quick access inside scripts. Since `GameManager` should be reset for each level, there is no `DontDestroyOnLoad`. It also holds reference to whether the level finished or not.
* `PlayerMovement` - moves the player automatically on the Z axis and side-wise according to the touch input, from which only horizontal values are taken. It also detects the collision with billboards and recomputes the formation and count of player squad. If the player collides with `EndSign`, the `GameManager's` `LevelFinished` is set to true which ends the level logic.
* `SquadManager` - manages the formation and number of units in squad. It uses a greedy algorithm for assigning units to slots. If there is not enough slots, it ends the computation but the count of units is still displayed correctly. There is 5 types of units -> 100-unit, 25-unit, 10-unit, 5-unit, 1-unit. Each unit has different collors according to `unitMaterials`.