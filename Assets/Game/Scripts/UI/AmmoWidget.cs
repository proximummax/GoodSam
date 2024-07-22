using TMPro;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _clipsText;
    private void Awake()
    {
        _ammoText.text = "0";
    }
    public void Refresh(int ammoCount, int clipsCount)
    {
        _ammoText.text = ammoCount.ToString();
        _clipsText.text = clipsCount.ToString();
    }
}
