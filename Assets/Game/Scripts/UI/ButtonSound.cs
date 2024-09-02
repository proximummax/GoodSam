using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    private Image _buttonView;
    private void Start()
    {
        _buttonView = GetComponent<Image>();
        if (_buttonView) _buttonView.sprite = MusicManager.MusicVolume == 1.0f ? _soundOn : _soundOff;
    }
  public  void OnClick()
    {
        PlayerPrefs.SetFloat("volume", PlayerPrefs.GetFloat("volume", 1.0f) == 1.0f ? 0.0f : 1.0f);
        MusicManager.MusicVolume = PlayerPrefs.GetFloat("volume", 1.0f);
        if (_buttonView) _buttonView.sprite = MusicManager.MusicVolume == 1.0f ? _soundOn : _soundOff;
    }
}
