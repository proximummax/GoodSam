using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void OnGameButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnCyberCityButtonClick()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
