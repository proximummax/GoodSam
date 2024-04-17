using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataSO", order = 1)]
public class WeaponData : ScriptableObject
{
    public BaseProjectile ProjectilePrefab;
    public int InitClipsCount;
    public int ProjectilesInClipCount;

    [HideInInspector] public int CurrentClipsCount;
    [HideInInspector] public int CurrentProjectilesCount;



}
