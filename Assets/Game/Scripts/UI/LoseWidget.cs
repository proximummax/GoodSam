using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWidget : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
    public void OnHomeButtonClick()
    {
        YG.YandexGame.CloseFullAdEvent += delegate { SceneManager.LoadScene(0); };
    }
    public void OnRestartButtonClick()
    {
        YG.YandexGame.CloseFullAdEvent += delegate { SceneManager.LoadScene(1); };
    }
}
