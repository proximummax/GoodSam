using TMPro;
using UnityEngine;

public class RoundWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        SetRound(UserDataStorage.Instance.Round);
    }
    public void SetRound(int round)
    {
        _text.text = round.ToString();
    }
}
