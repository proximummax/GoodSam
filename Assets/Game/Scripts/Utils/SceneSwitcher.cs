using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void OnGameButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnHomeButtonClick()
    {
        Cursor.lockState = CursorLockMode.Confined;
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene(0);
    }
}
