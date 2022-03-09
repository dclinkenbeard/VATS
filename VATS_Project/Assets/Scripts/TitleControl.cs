using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Navigate the main menu of the game. This class provides functions for accessing the two main scenes
///     of the game, Exploration Mode and Conservation Mode.
/// </summary>
public class TitleControl : MonoBehaviour
{
    /// <summary>
    ///     Load the "Exploration" scene. Here, marine life can be observed under user-specified environmental conditions.
    ///     Controllable sliders are presented in this mode where their values represent the ocean under variable
    ///     conditions such as temperature and depth, and marine life known to live under those conditions is
    ///     spawned accordingly.
    /// </summary>
    public void GoToSimulation(){
        SceneManager.LoadScene("ExplorationScene");
    }

    /// <summary>
    ///     Load the "Conservation" scene. Here, marine life can be observed under user-specified environmental conditions
    ///     as a function of time. A controllable slider is presented in this mode where the value represents environmental
    ///     conditions after the specified amount of time.
    /// </summary>
    public void GoToConservation(){
        SceneManager.LoadScene("ConservationScene");
    }
}
