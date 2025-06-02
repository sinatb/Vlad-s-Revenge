# Vlad's Revenge

## Overview

**Vlad's Revenge** is a roguelike game developed as the final project of Game design course at SBU. The GDD for this game is available at this [link](https://docs.google.com/document/d/10vQLFvC5UI_jXkI5mxsT048iPFIxke37xx8jNeeMCq0/edit?usp=sharing).

## Features 

### PCG
Rooms in this game are created with Cellular Automata algorithm. Parameters specify the number of times rules are applied, height and width of the room and also available styles for room generation.

![GeneratorParams](./screenshots/generatorParams.png)

### Dynamic Difficulty
A dynamic difficulty system has been implemented for determining the number and type of enemies in each room and each level.

### Perks and Effects
A total of 9 perks have been implemented in the game. Some perks offer flat stat increase (increase health, damage and speed) while others introduce effects on players attacks (life steal, damage over time and critical damage). For implementing these perks an extensive event system was implemented.

### Player Classes
There are in total 3 different classes availabe to the player. Each class offers a unique special ability. The mage class can invoke spells (like invoker in dota) the hunter can shoot extremely powerfull shots and the berseker ignores damage. For creating players in runtime something similar to builder pattern was used.

![HeroSelection](./screenshots/HeroSelection.png)

### Upgrade System
There is an upgrade system available for the player to increase attributes related to all classes like damage and health.

![Evolution](./screenshots/Evolution.png)

### Optimization
All the objects in the game are created with a pooling system. There are 2 types of object pools availabe. 

1. Style pool : for creating blocks used in map creation
2. Object pool : used for creating Enemies and projectiles

### AI
Enemies use simple rules for determining their behaviour at each moment. For pathfinding, A* algorithm is used to find path to player or tiles around the player.

## GamePlay

[youtube](https://youtu.be/vc-X2lN62e8)

## Future Plans

- Adding Sound Effects
- Adding FeedBack for player damage
