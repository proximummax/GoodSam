using UnityEngine;
using YG;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        YandexGame.StickyAdActivity(true);
    }
}
