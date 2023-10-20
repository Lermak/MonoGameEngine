# MonoGameEngine
Building an engine to learn the fundamentals of video game architecture

## Exemplary works
- [Beside Us Alone (GEJAM), 2021](https://fermak.itch.io/beside-us-alone)
- [Remote Work (LUDUM DARE 49), 2021](https://ldjam.com/events/ludum-dare/49/remote-work)

## Installation
- clone the repository, and open the project to let OmniSharp do it's thing.
- make sure to [Add nuget.org as a package source [msdn]](https://docs.microsoft.com/en-us/nuget/api/service-index) for your project if you don't have it (instructions follow)
  #### Visual Studio Code
  - open a terminal in the project directory and run
  ```
  dotnet nuget add source https://api.nuget.org/v3/index.json --name nuget.org
  ```
  
  #### Visual Studio (2019)
  - in Visual Studio (2019), this is done through "Tools" > "NuGet Package Manager" > "Package Manager Settings", searching "package sources" in the top left, and adding `nuget.org` with url `https://api.nuget.org/v3/index.json`
![Image showing Package Manager Settings](https://cdn.discordapp.com/attachments/722708774967574618/838102111032049674/unknown.png)

# Using the engine

# Overview
## Primary Engine Components
The primary building block of this engine are broken down as follows:
### Scenes
These are the core containers and represent a single "level".
### Game Objects
These are the indevidual actors, items, images, ext. that exist within the scene. The GameObject class is the most fundemental version, however there is also a WorldObject class for objects that are designed to be rendered, collide, and otherwise be interacted with by the player.
### Components
These are the data containers for GameObjects that store the variables and functional parts of a GameObject
### Behaviors
These are the actions that a GameObject performs. Each game loop they will perform each of their behaviors
### CollisionBehaviors
These are special types of behaviors that only occur when a collision is detected
## Scenes
When you want to create a new level you will start with the creation of a new Scene object. Scenes have two primary function that must be overridden
### Load Content
The first function that is required is `protected override void loadContent()`. This will be where you load all of your external resources into memory from the `ContentManager` provided by MonoGame. These resources will remain in memory until the scene is destroyed. 
### Load Objects
This is going to be where you build the level. As there is no GUI for this engine this part of development is very prodecural. The `InitWorldObject` and `InitGameObject` functions will handle most of the heavy lifting regarding initilizing your GameObjects and making them accessable in the scene.
### Scene Management
The `SceneManager` class will handle most interaction of the current scene, including fetching and adding new objects to the scene.
To access GameObjects once they have been loaded you just need to call `GetObject` from within the scene or `SceneManager.GetObject` from outside of the scene object. Objects are located by the tag that they are given during instanciation. 
For objects that you don't want to reference directly you can provide a blank name. This is only suggested for small things like projectiles that have a very short lifetime in the scene to begin with.
Adding new objects to the scene at runtime can be done with the `SceneManager.AddObject` function. Objects created this will will appear in the next frame of the game.
Changing the current scene is performed with the `SceneManager.ChangeScene` function. As part of this function the current scene will have its `OnExit` function called, and the new scene will have the `OnLoad` function called. These are overwriteable function that, at base, perform a fade-out and fade-in effect. 
You can also overwrite the `[Scene].SceneRunning` function which handles what occurs at each game loop. As standard this will have all GameObjects update themselves, add any new GameObjects to the list of GameObjects, and remove any destroyed GameObjects. 
Likewise there is a `[Scene].ScenePaused` function that has no default behavior. 
Those two functions are called based on the `SceneManager.SceneState` variable being set to Running or Paused respectively. 
## Game Objects
Game objects are, at the core, a container for multiple lists of other more foundational parts; components, behaviots, and collision behaviors. Each of these are given their own special handler.
### Component Handler
This is a fundemental part of a game object that handles loading, sorting, fetching, and updating the components of the object. This can be access from the `[GameObject].ComponentHandler` property.
### Behavior Handler
This is a fundemental part of a game object that handles loading, sorting, fetching, and updating the behaviors of the object. This can be access from the `[GameObject].BehaviorHandler` property.
### Collision Handler
This is a special type of component that interacts directly with the CollisionManager. Unlike the other two handlers, this one isn't required since not all objects have any need to collide. This can be accessed from the `[WorldObject].CollisionHandler` property. The GameObject class does not have this component by default.
## Components
Components are containers of data and functions to interact with that data. Alone a component will not do anything. It is a definition of attributes that a GameObject posesses that can then be utilized by behaviors and collision behaviors. The core components for the game engine are:
### Transform
This component will manage where in the world a GameObject exists, its size, and rotation.
### Sprite Renderer
This component, and its children, will manage what sprite to draw for an image, how offset it is from the object's Transform, what camera to render to, and any shaders that should be applied.
### Collider
This component, and its children, are required to interact with the `CollisionManager` and require an object to have a `CollisionHandler` if it needs to perform any work during a collision.
### Rigid Body
This component handles the modification of the Transform position and rotation based on velocity.
### Adding Components
A component can be added to a GameObject by calling the `[GameObject].AddComponent` function. 
E.X. `ComponentHandler.Add(new CollisionCircle("myBox", this, 10, new Vector2(), false));`
## Behaviors
A behavior is a delegate function following this signature: `void Sample(float dt, GameObject go, Component[] c = null)`. These functions are called each game loop on each GameObject and are the core of what makes anything happen in the game.
### dt
DT is the delta time, or time since the last frame. This is used to handle anything that sould occur over time. If you want something to move 300 units over a second and not 300 units per frame, this is the value you use.
### go
This is a reference to the GameObject calling the behavior. You can use it in conjuction with `GetComponent` to pull specific components from the object.
### c
This is a list of components that are outside of the calling GameObject's own list. This can often be left as a null array.
### Adding Behaviors
Behaviors can be added to a GameObject by calling the `[GameObject].AddBehavior` function, however is isn't quite as simple as a component. When you add a behavior you need to provide it with the values that make up a `Behavior` object. That object will consist of a name, a deligate function, and the list of external components to be given to the deligate function when it is called each loop.
E.X. `BehaviorHandler.Add("MoveForward", Behaviors.MoveTowardRotation, null);`
## Collision Behaviors
Collision behaviors are another type of deligate function that have the following signature `void Sample(Collider a, Collider b, Vector2 p)`. These functions are called by the `CollisionHandler` whenever an appropriate collision is detected.
## a
This is the collider of the GameObject looking for collision.
## b
This is the collider of the GameObject that was collided with.
## p
This is the minimum penitration vector as detemined by an SAT collision algorithm.
## Handling Collisions
In order for a GameObject to do anything with a collision it will need a `CollisionHandler` component. Once you have that component you can add collision actions as follows:
`CollisionHandler.AddCollisionAction(new CollisionActions("myBox", new List<string> { "myBox", "TileWall" }, new List<CollisionAction> { CollisionBehaviors.UndoMinPen }));`
`CollisionActions` define what collider is checking for collisions, the list of tags that other colliders can have to trigger these behaviors, and the list of behaviors to perform. In this way each collider on the GameObject will perform its own collision checks.

## TL;DR
To use the engine you need to create new `Scene` objects. Within those scenes you will create `GameObject`s that represent eveything in the world. Those GameObjects will have `Component`s that determine what values and attributes they posess and `Behavior`s that define what to do with those values each game loop. 
