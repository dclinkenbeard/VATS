using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadExploration()
    {
        SceneManager.LoadScene("Isaac_Scene");
    }
    public void LoadFEV()
    {
        SceneManager.LoadScene("FEV");
    }
    public void LoadHome()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
