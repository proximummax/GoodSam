using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : BaseHealthBar
{
    [SerializeField] private Image _foreground;

    public override void SetPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        _foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
