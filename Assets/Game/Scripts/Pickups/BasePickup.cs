using UnityEngine;

public class BasePickup : MonoBehaviour
{
    public enum EPickupType
    {
        Weapon, Health, Ammo
    }
    [SerializeField] private EPickupType _type;
    public EPickupType Type { get { return _type; } }
}
