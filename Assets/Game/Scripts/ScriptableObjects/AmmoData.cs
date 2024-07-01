using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/AmmoDataSO", order = 1)]
public class AmmoData : ScriptableObject
{
    public int Bullets;
    public int Clips;
    public bool Infinite;

    public void Init(AmmoData initData)
    {
        Bullets = initData.Bullets;
        Clips = initData.Clips;
        Infinite = initData.Infinite;
    }
}
