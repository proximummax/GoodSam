
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoseWidget : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
       // CrazySDK.Game.GameplayStop();
        //Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
    public void OnHomeButtonClick()
    {

     /*   CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
        {
           
        }, (error) =>
        {

            SceneManager.LoadScene(1);
        }, () =>
        {
            SceneManager.LoadScene(1);
        });*/

        YandexGame.Instance.CloseFullscreenAd.AddListener(delegate { SceneManager.LoadScene(0); });
        YandexGame.FullscreenShow();
    }
    public void OnRestartButtonClick()
    {
        /* CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
         {

         }, (error) =>
         {
             SceneManager.LoadScene(2);
         }, () =>
         {
             SceneManager.LoadScene(2);
         });*/

        YandexGame.Instance.CloseFullscreenAd.AddListener(delegate { SceneManager.LoadScene(1); });
        YandexGame.FullscreenShow();
    }
}
