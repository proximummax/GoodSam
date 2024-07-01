using UnityEngine;

[RequireComponent(typeof(WeaponOwnerComponent))]
public class ReloadComponent : MonoBehaviour
{

    private WeaponOwnerComponent _weaponOwner;
    [SerializeField] private Animator _rigController;
    [SerializeField] private WeaponAnimationEvents _animationEvents;
    private void Start()
    {
        _weaponOwner = GetComponent<WeaponOwnerComponent>();
    }
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
        _rigController.SetTrigger("reload_weapon");
    }
    public void AttachMagazine()
    {
        Debug.Log("TODO: ATTACH MAGAZINE");
    }
}
