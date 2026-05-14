using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUi : MonoBehaviour
{
    public void ReturnToScene()
    {
        SceneManager.LoadScene(0);
    }
}
