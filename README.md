## Prefabs

The project contains prefabs for:

- `EnemyUnits` - some predefined types of enemies - male, female, different color
- `Formations` - some predefined formations - circle, line, square
- `Managers` - level managers
- `PlayerUnits` - some predefined types of units for player - male, female, different color
- `Signs` - some predefined signs
- `Tiles` - some predefined floor tiles
- `SignSpawnPoint` - default starting point where signs should be spawned

## Scenes

### Main

Basic UI for main menu with transitions to level menu and survival mode. The application can be closed via this scene.

### LevelMenu

Basic UI for level menu with two levels and a transition to a main menu.

### Level Scenes

Each level consists of:

- `Camera` - main camera with Camera Controller script attached
- `FloorManager` - manager for spawning floor tiles
- `GameManager` - manager for the game states
- `UIManager` - manager for UI transitions and states
- `SignManager` - manager for spawning signs
- `SignSpawnPoint` - default starting point where signs should spawn
- `PlayerManager` - manager for player squad
- `EnemyManager` - manager for enemy squads

Level is finished when player reaches the `EndSign`, which also fires transition to Jump Animation.

#### Construction of level

To create a level, user should place into scene all required components described in paragraph about level. When all necessary components are placed, user can adjust all important variables of each component from inspector. 

To spawn signs and enemies in regular mode, user should put the same amount (!) of enemy definitions in `EnemyManager - Squad Configs List` (in order of which squads should be spawned) and sign definitions in `SignManager - Sign Pairs List` (also in order of which signs should be spawned). User can define arbitrary number of signs in row but 2-3 sign in row seem to look the best.

To spawn signs and enemies in survival mode, user should put all prefabs for signs which should be used for spawning in SignManager, variable `Prefabs` and all squad templates for enemies which should be used in Squad Configs List (here it doesn't matter what count user defines for each squad template because the count will be generated via Equation Oracle)


### Survival

Survival Mode. This level has no ending except dying. After death, a UI PopUp is shown with the player's achieved score and player can decide if he wants to try again or quit to main menu.

The principle of creating the world is very similar as in basic levels except the enemies and math billboards are generated each `VerticalStep` with the help of `Equation Oracle`. Survival mode can be turned on in Game Manager (also from inspector).

## Classes

### Animators

#### CharacterAnimator

Abstract class for fetching Animator component and same operations / variables for every possible Animator class

#### PlayerAnimator

Child of `CharacterAnimator` - allows player units to fire jump animation.

#### EnemyAnimator

Child of `CharacterAnimator` - allows enemy units to fire run animation.

### Equations

#### Equation

Basic class for equations. It holds one operation symbol and one operand. If equations would be more complex, it would be implemented as a parser.

#### EquationOracle

`Equation Oracle` chooses the operation of math billboard and the number of enemies spawned after this billboard. It generates random equations until the maximum of player units after colliding with billboard is bigger than 2. The EquationOracle computes number of units in next enemy squad  as a random value in range `[_successPercentage * maxPlayerUnits, maxPlayerUnits)` so that the number of player units after colliding with enemies is at least one when he choose the sign with better equation.

#### Operation

Basic classes for mathematical operations.

### Manager

#### EnemyManager

Manager for enemy squads. It hold object pools for every used prefab in that level and provides method for returning object pool for given prefab. Its main job is to spawn `EnemySquad` whenever equation signs are spawned (via event). If survival mode is enabled, it asks Equation Oracle for enemy count in one squad and then initializes the squad to be later spawned. 

#### FloorManager

Manager, which takes care of spawning floor tiles. It has its own object pool from which it spawns tiles.

#### GameManager

Manager for game state. Its main job is to listen for when level should be finished/failed and appropriate pop up shown. When survival mode is enabled, it also computes players score.

#### PlayerManager

(SIDENOTE: Since the game logic is that player can have only one squad, the PlayerManager is child of Squad while also being a manager ofitself. If there could be more player squads, it would be implemented similar to EnemyManager and then there could be an abstract class/interface for functionality which is similar/same for ObjectManagers but for this project it felt like a little overengineering)

Manager for player squad. It holds own object pools for every prefab used for player unit in that level. Main job is to respawn player squad when needed and decide what should happen when the squad collides with sign.

#### SignManager

Manager for spawning signs. It holds own object pools for every sign prefab used in that level. Its main job is to spawn signs each `VerticalStep` which are given on input. After all signs defined by user are spawned, it spawns an EndSign for ending the level. If survival mode is enabled it asks Equation Oracle for equations and initializes equations to be spawned. 

#### UIManager

Manager for UI transitions and states. Its main job is to listen when to show which pop up.

### Mover

#### EnemyMovement

Class for updating direction according to players position and deciding when to move. It also detects collisions with player.

#### PlayerMovement

Class for updating direction according to input and moving the player unit. It also detects collisions with signs and sends trigger to player manager to handle collision for the whole squad.

### ObjectPooler

#### ObjectPooler

Base class for different kinds of object pools.

#### SignObjectPooler

Child of object pooler, defines an object pool for signs.

#### TileObjectPooler

Child of object pooler, defines an object pool for tiles.

#### UnitObjectPooler

Child of object pooler, defines an object pool for character units.

### PoolableObjects

Base class for objects, which can appear in object pool.

#### Sign

Child of PoolableObjects. It holds an equation and text reference which is updated whenever sign is initialized. Each sign also holds reference to each neighbors so upon collision one sign can release all sign in that row.

#### Tile

Child of PoolableObjects. It checks for if its behind camera so that particular tile can be released to pool and new one spawned.

#### Unit

Child of PoolableObject. Takes care of character units in squads.

### ScriptableObjects

#### CharacterDictionary

List which hols pairs of `identifier` and `prefab` so user can specify when and which prefab should be used - e.q. unit values in squad.

#### SquadTemplate

Definition for how a squad should look like. Can specify prefab for formations, character dictionary and angle at which units will be spawned.

### Signs

#### SignTemplate

Class for defining how one sign should look like - it holds a prefab and equation.

### Squad

#### Squad

Base class for managing units in squad. It holds a character dictionary for spawning units and a formation which should be held. Also holds count and angle of units. Squad class's main job is to decide which text reference in squad should be used and generate squad according to formation and character dictionary - for generating it uses a basic greedy algorithm (variant of cashier's algorithm).

#### EnemySquad

Child of Squad. Class for managing one enemy squad. It can be initialized on the go from config file and it takes care of spawning and respawning the squad.

#### SquadDefinition

Class for coupling together SquadTemplate and count of units in that squad.

### Other

#### CameraController

Class which controls movement of camera according to players position.

#### InputController

Class which decides how input should be computed.

#### SafeArea

Class for resizing buttons to fit in safe area.

#### SceneChanger

Class which manages transitions between scenes.

#### Utils

Static class for basic functions/algorithms.
