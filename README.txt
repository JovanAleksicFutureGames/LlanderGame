Project Llander
A Prototype

By: Jovan Aleksic

OVERVIEW:
Project Llander is a sidescrolling lander type of game where the player navigates asteroid mazes and tries to reach the end.The ultimate goal is to land on the final platform found on level 4.
The player can pick up extra fuel canisters along the way and repair kits that add 1 point of health to the player. The player is supposed to fly their ship by controlling the ships's booster and controlling the rotation with occasional boosts in speed through the "jump" functionality. 

HOW TO PLAY:
The game is played in a keyboard.
The player's rotation is controlled with the A and D keys. The player can boost by holding the space key, finally the player can "jump" by pressing the Left Shift key. You pause the game with the Esc key. 

DATA PERSISTANCE:
The game persists data through a custom serialization system I made some time ago. The code for data persistence is based on the one taught in courses by GameDev.tv with minor adjustments made by me. The system is just bloat at this point because I couldn't take full advantage of something created for an RPG.
The core objects are all collected in one prefab. Managers persist and DoNotDestroyOnLoad objects. 

SCENE FLOW:
The first scene in "Scenes" folder you should open is MainMenu. From there the scenes should go over Level1, Level2, Level3, Level4 and back to the Main Menu. Notice that you as a player do not control anything in the scene flow outside of entering the game and returning to the main menu through the pause menu, as well as returning through the victory screen.

CODE STRUCTURE AND GAMEPLAY:
Two scripts drive the behaviour of the player - Controller and Motor (PlayerController.cs and PlayerMotor.cs). The player controller takes in the player input, as well as whether or not the player is alive. PlayerController also hosts the PlayerData scriptable object that stores the amount of fuel the player has and the player's health. If the player runs out of health, the ship explodes and the player will find themselves at the start of the level. If the player runs out of fuel, they will lose control over the craft and it will plummet down while spinning. The player ship will then explode upon contact with any surface. The player goes through a series of portals. The portal code was also written by me a while ago based on the code provided by GameDev.tv. I reused the code from one of my side projects, and it works, although there are certain hickups. 

The code itself compiles and works properly and is cleaned of any unnecessary functons. 
The structure of the code for the saving system and the portal system (that exists outside of my scene handler) are all importen from an earlier project I worked on. This project is heavily based on a course done by GameDev.TV and as such will feature a lot of similarities with their stuff (namely the RPG Core course). 

The game also has an enemy turret that fires rockets at the player.
The player will lose a health point every time they collide with the environemnt or the rockets. The collision doesn't damage the player upon contact with portals or the final platform.

MANAGERS:
Game Manager, UI Manager, Audio Manager and Scene Handler exist as .cs files in the scripts folder. The game manager controls the flow of the game, UI Manager handles all of the UI elements and interaction and the Audio Manager handles audio effects. The scene handler controls the transitions in the menus. 


Bugs left over:
Rockets can deal damage to the player two times in a row due to the speed with which they self-destruct.
The music gets disabled after the player "wins" the game and doesn't start back up unless the player "restarts" the game. 
The game builds, but cannot be played on a cumputer with two screens.