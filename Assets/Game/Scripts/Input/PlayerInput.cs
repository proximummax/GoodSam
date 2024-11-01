//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Game/Scripts/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9a8a0ac3-4887-4be5-8ca8-c0261b82871b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""24dd2d5b-62c2-45a4-8ff4-d3aa62252770"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""c31642a6-da50-4f77-b81d-929a2dec1aef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""43a6da6a-6f1f-45ac-96af-1ac4ab7ddb57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""cbb9c3c0-7b1c-4585-929b-65b450ca20e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""beedad2c-97ae-4995-ad24-95009dafdc61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d538623e-c8ab-463f-8aef-d19602ec0383"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Holster"",
                    ""type"": ""Button"",
                    ""id"": ""7a232962-3865-46ae-a140-50107379b582"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectWeapon_1"",
                    ""type"": ""Button"",
                    ""id"": ""f30766fe-bdaf-4eb2-8cb9-bdd4004efc3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectWeapon_2"",
                    ""type"": ""Button"",
                    ""id"": ""eefc8084-8dc2-45ce-8b8f-1f99ef38cb5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d38dbf71-e9dc-4372-bda2-462785627679"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d16ac3bf-f4ed-4d1f-b47b-2060d55610ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1cc510fc-dbca-46fe-9e09-3104f8a37920"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a693ecaa-dc9a-47ae-ab94-5be03817bfac"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""06cbc229-e68b-4874-b0e9-926c1902e848"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""ed287c6a-679e-48a1-a5d5-53d571c9561b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""229fa27d-7aef-4c9a-bd48-d89a83d4e4ab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""b6b24c7d-04b4-480c-95ac-8b3dbf97910f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cae1f33b-f1c2-4bb9-a204-adf090a72142"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0da4884-093b-4ebd-b31c-6a52247cd98a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""891e95e9-33bb-492f-a6d8-9ebd2c8e3ba9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9a3326b-ed99-4433-9a02-b7e44eb410a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a6fee53-9dfb-402e-878b-8cb9fe56e00b"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Holster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""853fec48-a2d5-482d-8f03-89fc02058b84"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SelectWeapon_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c77428c8-1f0a-4f34-b8e5-62fa44b0078e"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SelectWeapon_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30ad7f0a-05b5-437c-9bef-485e1276caff"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50b183ff-a208-4b97-98c2-4f2249183b17"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Holster = m_Player.FindAction("Holster", throwIfNotFound: true);
        m_Player_SelectWeapon_1 = m_Player.FindAction("SelectWeapon_1", throwIfNotFound: true);
        m_Player_SelectWeapon_2 = m_Player.FindAction("SelectWeapon_2", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Holster;
    private readonly InputAction m_Player_SelectWeapon_1;
    private readonly InputAction m_Player_SelectWeapon_2;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Holster => m_Wrapper.m_Player_Holster;
        public InputAction @SelectWeapon_1 => m_Wrapper.m_Player_SelectWeapon_1;
        public InputAction @SelectWeapon_2 => m_Wrapper.m_Player_SelectWeapon_2;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Holster.started += instance.OnHolster;
            @Holster.performed += instance.OnHolster;
            @Holster.canceled += instance.OnHolster;
            @SelectWeapon_1.started += instance.OnSelectWeapon_1;
            @SelectWeapon_1.performed += instance.OnSelectWeapon_1;
            @SelectWeapon_1.canceled += instance.OnSelectWeapon_1;
            @SelectWeapon_2.started += instance.OnSelectWeapon_2;
            @SelectWeapon_2.performed += instance.OnSelectWeapon_2;
            @SelectWeapon_2.canceled += instance.OnSelectWeapon_2;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Holster.started -= instance.OnHolster;
            @Holster.performed -= instance.OnHolster;
            @Holster.canceled -= instance.OnHolster;
            @SelectWeapon_1.started -= instance.OnSelectWeapon_1;
            @SelectWeapon_1.performed -= instance.OnSelectWeapon_1;
            @SelectWeapon_1.canceled -= instance.OnSelectWeapon_1;
            @SelectWeapon_2.started -= instance.OnSelectWeapon_2;
            @SelectWeapon_2.performed -= instance.OnSelectWeapon_2;
            @SelectWeapon_2.canceled -= instance.OnSelectWeapon_2;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnHolster(InputAction.CallbackContext context);
        void OnSelectWeapon_1(InputAction.CallbackContext context);
        void OnSelectWeapon_2(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
