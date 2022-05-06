# VATS

======

## Table of Contents
1. [Overview](#Overview)
2. [Product Spec](#Product-Spec)
3. [Schema](#Schema)
4. [Dependencies](#Dependencies)

## Overview

Virtual Aquarium Tank System is a unity based game that allows user to explore sea life using simulation and finding out how different factors affect sea animals.

### Description

## Product Spec

### 1. User Stories (Required and Optional)

**Required Must-have Stories**

- [ ] ----
- [ ] ----

**Optional Nice-to-have Stories**

- [ ] ----
- [ ] ----

### 2. Scene Archetypes

* Exploration Scene
   * ----
   * ------
   
* Conservation Scene
   * ----
   * ------


### 3. Navigation
[Mostly the file structure goes here and as well as in-game instructions
** --

### [BONUS] Interactive Prototype

## Schema 
### Models

**FEVs**
| Property      | Type      | Description                                                      |
|---------------|-----------|------------------------------------------------------------------|
| id            | int       | Unique id for marine life                                        |
| name          | String    | Marine life name                                                 |
| sci           | String    | Scientific name                                                  |
| type          | String    | Marine life type                                                 |
| diet          | String    | Marine life diet description                                     |
| habitat       | String    | Marine life habitat                                              |
| minSize       | Int       | Minimum size for marine life                                     |
| maxSize       | Int       | Maximum size for marine life                                     |
| minTemp       | Int       | Minimum living temperature                                       |
| maxTemp       | Int       | Maximum living temperature                                       |
| minDepth      | Int       | Minimum living depth                                             |
| maxDepth      | Int       | Maximum living depth                                             |
| minAcidity    | Int       | Minimum living acidity                                           |
| maxAcidity    | Int       | Maximum living acidity                                           |
| minPollution  | Int       | Minimum living pollution                                         |
| maxPollution  | Int       | Maximum living pollution                                         |
| range         | String    | Description of marine life living situations                     |
| status        | String    | Marine life extinction status                                    |
| lowerLimit    | Int       | minimum spawning height                                          |
| upperLimit    | Int       | maximum spawning height                                          |

## Dependencies
### used crest for ocean: 
- https://github.com/crest-ocean/crest

### Boids Resources:
- https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
- https://www.youtube.com/watch?v=bqtqltqcQhw&ab_channel=SebastianLague
- http://www.cs.toronto.edu/~dt/siggraph97-course/cwr87/
- https://www.bitshiftprogrammer.com/2018/01/how-to-animate-fish-swimming-with.html
            
### Information about the ocean and acid levels
- https://earth.org/ocean-acidification-linked-to-plastic-pollution/

