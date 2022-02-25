# VATS

======

## Table of Contents
1. [Overview](#Overview)
1. [Product Spec](#Product-Spec)
1. [Wireframes](#Wireframes)
2. [Schema](#Schema)
3. [Dependencies](#Dependencies)

## Overview

Virtual Aquarium Tank System is a unity based game that allows user to explore sea life using simulation and finding out how different factors affect sea animals.

### Description


### App Evaluation
[Evaluation of your app across the following attributes]
- **Category:** 
- **Mobile:** 
- **Story:**
- **Market:** 
- **Habit:** 
- **Scope:** 


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


### 3. Navigation

**Tab Navigation** (Tab to Screen)

* Logout -> login screen
* user details -> user details page
* float button (bottom of the page) "Home"
* Back button

**Flow Navigation** (Screen to Screen)

* Login
   * Home Screen
   * Sign up screen (incase we don't get access to csumb api)
* Home Screen
   * Login
   * Mapping
   * Class Schedule
   * Events
   * Info
   * User details page
* Mapping
    * Home Screen
* Class Schedule
    * Home Screen
    * Mapping (later on)
* Event Page
    * Home Screen
    * Event details page
* Info Page
    * Home Screen
    * Resources details page

## Wireframes
[Add picture of your hand sketched wireframes in this section]
<img src="" width=600>
* Using figma

### [BONUS] Digital Wireframes & Mockups

### [BONUS] Interactive Prototype

## Schema 
### Models

**User**
| Property   | Type   | Description                                                          |
|------------|--------|----------------------------------------------------------------------|
| userId     | Int    | Unique ID associated with a user                                     |
| username   | String | Unique name associated with user                                     |
| email      | String | Campus/CSUMB Email.                                                  |
| profileImg | file   | Profile picture of user. Could also just use image of email account. |
| Password   | String | Password to log in. Maybe unnecessary if logging in with email.      |

**Events**
| Property   | Type     | Description                                                          |
|------------|----------|----------------------------------------------------------------------|
| eventName  | String   | Event name                                                           |
| eventDate  | DateTime | Date of event                                                        |
| eventLink  | String   | Link for more event details                                          |
| creatDate  | DateTime | Date the event was posted                                            |
| eventDesc  | String   | Brief description of the event                                       |

### Networking
- 

#### Network Requests


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

