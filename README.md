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
Campus buddy is a navigation app to navigate you through the campus and helps you connect to your campus community. You will be able to login using your school credentials and land on home screen. Where you will find mapping, events, class schedule, info, user details and logout options. Mapping will help you enter building number or name to navigate to it. Class schedule will import your schedule from csumb website or let you manually add classes there on your schedule. Events will let you browse through upcoming events and see their details. Info will let you browse through your school resources and access their info with one click. Logout will take you back to login screen and user details will show you your email and username.

### App Evaluation
[Evaluation of your app across the following attributes]
- **Category:** Navigation app
- **Mobile:** IOS app primarily
- **Story:** Our app allows users to easily navigate through the campus and be able to access their class schedules. Also lets them see the upcoming events on campus and information about other campus stuff.
- **Market:** Universities and students
- **Habit:** This app can be used often if students want to access their schedule or need help navigating to classes in the start of college and later on it depends on how much information they would like on new events or other info sessions.
- **Scope:** Start with CSUMB expanding to other campuses. Potential of it being used as primary campus app.

## Product Spec

### 1. User Stories (Required and Optional)

**Required Must-have Stories**

- [x] Log in/sign up using CSUMB credentials. (initiating database using Parse) - Shawn Deppe
- [x] Land on Homescreen with navigation items. - Isidro Perez, Bryan Fowles
- [x] Be able to access my weekly class schedule (option: google calander api) - Hamza Saleem, Miguel Espitia
- [x] Be able to navigate to my class through in-app navigation (google map api) - Denize Patric Ignacio, Hamza Saleem
- [x] Be able to manually navigate to the building specified by user (In Mapping Section) - Denize Patric Ignacio, Alexis Caasi
- [ ] Be able to receieve notifications about alerts/events going on right now - Alexis Caasi
- [x] Be able to access campus resources information - Armon Bakhtar
- [x] Be able to access upcoming events - Efrain Pamatz
- [x] Be able to stay logged in - Roober Gerard Cruz, Carmelo Hernandez
- [ ] Be able to logout of the app - Isidro Perez

**Optional Nice-to-have Stories**

- [ ] Be able to click on a class from class schedule part and it navigating you to the building
- [ ] Connect to CSUMB System
- [ ] Visitor version, not have to log in. 
- [ ] Parking spaces and how to pay for it.

### 2. Screen Archetypes

* Log in page
   * Log in by selecting my campus and entering my CSUMB credentials
   * Be able to logout of the app
* Sign up page
   * Be able to singup using csumb credentials (incase we don't get access through api)
* Home Screen / Navigation Center
    * Land on Homescreen with navigation items.
    * Be able to stay logged in
    * Be able to receieve notifications about alerts/events going on right now
* Class Schedule
    * Be able to access my weekly class schedule (optional)
    * Manually add classes (incase csumb doesn't give us access)
* Mapping
    * Be able to navigate to my class through in-app navigation
    * Be able to manually navigate to the building specified by user (In Mapping Section)
* Events Panel
    * Be able to access upcoming events
* Info Panel
    * Be able to access campus resources information

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
<img src="https://i.ibb.co/b69X4GP/Initial-wireframe.png" width=600>
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
- Google map & calendar API

#### Network Requests

- Log in User
    - (Read/GET) Get user info

- Events Screen
    - (Read/GET) Show all created Events
    - (Create/POST) Add an Event
    - (Delete) Delete an Event a user has created

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

## Sprint 1
### User registers and logs in
<img src="http://g.recordit.co/0pdR7ZURCs.gif" width=400>


## Sprint 2
### User logs in and stays logged in
<img src="https://i.imgur.com/0UsBNEJ.gif" width=400>

### User can view a calendar on top half of screen, also register and view classes on bottom half of screen
<img src="https://s3.gifyu.com/images/sprint3.gif" width=400>

## Sprint 3
<img src="http://g.recordit.co/UEWnXZNetp.gif" width=400>

## Sprint 4
<img src="http://g.recordit.co/k0q7r9Ehu7.gif" width=400>
