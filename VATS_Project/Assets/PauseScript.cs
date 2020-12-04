using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject askMainMenu;
    public GameObject askQuitMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        askMainMenu.SetActive(false);
        askQuitMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
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
