using UnityEngine;

[RequireComponent(typeof(BaseWeaponOwnerComponent))]
public class ReloadComponent : MonoBehaviour
{
    private static readonly int RELOAD_WEAPON = Animator.StringToHash("reload_weapon");
    
    [SerializeField] private Animator _rigController;
    [SerializeField] private WeaponAnimationEvents _animationEvents;

    public bool IsReloading { get; private set; } = false;

    private void OnEnable()
    {
        _animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }
    private void OnDisable()
    {
        _animationEvents.WeaponAnimationEvent.RemoveAllListeners();
    }
    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "Reload":
                AttachMagazine();
                break;
        }
    }
    public void SetReloadTrigger()
    {
        _rigController.SetTrigger(RELOAD_WEAPON);
        IsReloading = true;
    }

    private void AttachMagazine()
    {
        IsReloading = false;
    }
}
