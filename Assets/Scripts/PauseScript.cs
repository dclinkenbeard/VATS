using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject askMainMenu;
    public GameObject askQuitMenu;
    public CameraMovement cameraMovement;
    public bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else if(!(askMainMenu.activeSelf || askQuitMenu.activeSelf))
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        cameraMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        askMainMenu.SetActive(false);
        askQuitMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        cameraMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        askMainMenu.SetActive(false);
        askQuitMenu.SetActive(false);
    }

    // QUIT BUTTON
    public void AskQuitButton()
    {
        pauseMenu.SetActive(false);
        askMainMenu.SetActive(false);
        askQuitMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    // MAIN BUTTON
    public void AskMainButton()
    {
        pauseMenu.SetActive(false);
        askMainMenu.SetActive(true);
        askQuitMenu.SetActive(false);
    }

    public void MainButton()
    {
        SceneManager.LoadScene(0);
    }
}
