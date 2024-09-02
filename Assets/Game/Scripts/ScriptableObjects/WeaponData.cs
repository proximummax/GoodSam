using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon/WeaponDataSO", order = 1)]
public class WeaponData : ScriptableObject
{
    public BaseWeapon Weapon;
    public Animation ReloadAnim;
}
