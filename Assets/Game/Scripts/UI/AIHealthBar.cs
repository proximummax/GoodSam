using UnityEngine;
using UnityEngine.UI;

public class AIHealthBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private Image _background;
    [SerializeField] private Image _foreground;

    private void LateUpdate()
    {
        Vector3 direction = (_target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
        _foreground.enabled = !isBehind;
        _background.enabled = !isBehind;

        transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
    }
    public void SetPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        _foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
