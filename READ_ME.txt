Dual Stick Shooter:

-Spaceship will be controlled using a controller.
-Right controll will controll the spaceship movements and left one has a turrent movements and direction
-There are two types of enemies: One that has a random movement throughout the display and will not follow the
player. Another one which will follow the the player anywhere it goes unless player kills it.
-Both enemies will be instantiated in random times and each creates a funky sound when they are created.
-There will be treasures randomely displayed whick player can pick them up one at a time to fill up its inventory list.
-Once the player picked up all pickup Items,it will display an animation that these items were crucial items
to water its plants :)
-I created a loot table and it will be used whenever player kills an enemy.
In this loot table, it uses s ScriptableObject.
It loads the prefabs I created under this path : Assets/Resources/Treasures.
each of these items have different chances to show up ( coins has the most chance vs. Special_Turret with the least chance)
These items have their own tag as "Treasures" which player can pick up and it has nothing to do with the " Pick Up" items.


