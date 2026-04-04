using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Вышли из игры");
    }
}
