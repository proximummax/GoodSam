
using UnityEngine;

public class PauseWidget : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
}
