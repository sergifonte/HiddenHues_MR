using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUi: MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exiting the game . . .");
        Application.Quit();
    }
}
