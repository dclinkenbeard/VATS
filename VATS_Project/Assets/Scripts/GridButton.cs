using UnityEngine;
using UnityEngine.SceneManagement;

public class GridButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene("GridView");
    }
}
