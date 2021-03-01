// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""107b4469-3777-43c1-a908-83e28dbd285f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""aa921d87-7dae-4f58-b9d1-4e6729add4be"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""613feff7-8f7a-4c81-8f64-e309594a396d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""90310259-5df6-401a-a3f6-5d947d39a645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Let_Go"",
                    ""type"": ""Button"",
                    ""id"": ""10c63240-8a61-4a85-823a-32871738de00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""feb0bf83-eed5-4ada-ad2b-682fdaebc60a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""3a036df7-fd48-4141-bc25-b7d2542f975c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Shield"",
                    ""type"": ""Button"",
                    ""id"": ""ec1122cb-fa74-4a81-a7f3-c1901720b40f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""ab47b250-5606-4dbf-810b-ccca2ea0abe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b69310f0-4b70-4874-b886-6dc5845cbcc7"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69f77c9c-e07c-4f30-860a-624606f1e1e7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0f399fca-296b-49e2-a22b-b30c15bf30da"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ef547b9a-d62a-4fe5-8d75-4b3382f1d2cc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bc36a7bb-36b7-4d0c-a864-730863529b9e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d48f1d3e-2ed3-403f-9c0d-09bfb48d4e7a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f716f71d-0849-4a25-b138-b38c3a78fe5b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a7598887-ffa1-40e2-b507-64609cbf6dd2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6011239f-f8e4-4322-b509-ff1914f9720f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f77358e-2e07-4596-bc64-80b3baea30cb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfcade32-8546-4201-9e76-fa7153ea4c9b"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Let_Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca65ac86-cf91-44da-ac6f-06012d49fbd3"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Let_Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4b504ea-fd45-475d-91ed-56f8514df76c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ce50ac8-019e-43d7-881a-19a209754713"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""960edd5b-290e-4553-b29f-70edf900d45f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db9a3754-38d9-440b-9f57-13658b8b4f34"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da1ef43f-58af-4a69-9ba0-d36ea05edb39"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24945b83-1a60-439a-a048-1b1af4941c77"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b18ef27a-03b6-4072-8a34-d62460cf789b"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bf948e7-350b-4671-9908-51b432468fff"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Movement = m_CharacterControls.FindAction("Movement", throwIfNotFound: true);
        m_CharacterControls_Dash = m_CharacterControls.FindAction("Dash", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_Let_Go = m_CharacterControls.FindAction("Let_Go", throwIfNotFound: true);
        m_CharacterControls_Interact = m_CharacterControls.FindAction("Interact", throwIfNotFound: true);
        m_CharacterControls_Attack = m_CharacterControls.FindAction("Attack", throwIfNotFound: true);
        m_CharacterControls_Shield = m_CharacterControls.FindAction("Shield", throwIfNotFound: true);
        m_CharacterControls_PauseGame = m_CharacterControls.FindAction("PauseGame", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Movement;
    private readonly InputAction m_CharacterControls_Dash;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_Let_Go;
    private readonly InputAction m_CharacterControls_Interact;
    private readonly InputAction m_CharacterControls_Attack;
    private readonly InputAction m_CharacterControls_Shield;
    private readonly InputAction m_CharacterControls_PauseGame;
    public struct CharacterControlsActions
    {
        private @PlayerControls m_Wrapper;
        public CharacterControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterControls_Movement;
        public InputAction @Dash => m_Wrapper.m_CharacterControls_Dash;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @Let_Go => m_Wrapper.m_CharacterControls_Let_Go;
        public InputAction @Interact => m_Wrapper.m_CharacterControls_Interact;
        public InputAction @Attack => m_Wrapper.m_CharacterControls_Attack;
        public InputAction @Shield => m_Wrapper.m_CharacterControls_Shield;
        public InputAction @PauseGame => m_Wrapper.m_CharacterControls_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Dash.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDash;
                @Jump.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Let_Go.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLet_Go;
                @Let_Go.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLet_Go;
                @Let_Go.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLet_Go;
                @Interact.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Shield.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnShield;
                @Shield.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnShield;
                @Shield.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnShield;
                @PauseGame.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Let_Go.started += instance.OnLet_Go;
                @Let_Go.performed += instance.OnLet_Go;
                @Let_Go.canceled += instance.OnLet_Go;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Shield.started += instance.OnShield;
                @Shield.performed += instance.OnShield;
                @Shield.canceled += instance.OnShield;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLet_Go(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnShield(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
