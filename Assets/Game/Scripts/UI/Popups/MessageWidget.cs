using System.Collections;
using TMPro;
using UnityEngine;

public class MessageWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _showSpeed = 2.0f;

    private Color _defaultColor = new Color(0.1058824f, 0.9333334f, 0.937255f, 1.0f);
    private Color _invisibleColor = new Color(0.1058824f, 0.9333334f, 0.937255f, 0.0f);

    public void ShowMessage(string message)
    {
       // _text.text = message;
        StartCoroutine(ShowMessageEffect());

    }
    private IEnumerator ShowMessageEffect()
    {
        float tick = 0f;
        while (_text.color != _defaultColor)
        {
            tick += Time.deltaTime * _showSpeed;
            _text.color = Color.Lerp(_invisibleColor, _defaultColor, tick);
            yield return null;
        }

    }
}
