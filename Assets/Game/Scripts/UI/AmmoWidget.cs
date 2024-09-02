using TMPro;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    private void Awake()
    {
        _ammoText.text = "0";
    }
    public void Refresh(int ammoCount)
    {
        _ammoText.text = ammoCount.ToString();
    }
}
