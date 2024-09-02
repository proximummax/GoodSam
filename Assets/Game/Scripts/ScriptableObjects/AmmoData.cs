using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/Weapon/AmmoDataSO", order = 1)]
public class AmmoData : ScriptableObject
{
    public int Bullets;

    public void Init(AmmoData initData)
    {
        Bullets = initData.Bullets;
    }
}
