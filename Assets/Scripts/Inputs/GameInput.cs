// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""c78230b3-16a4-412a-968c-8a09a1bfa152"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""591c6909-3130-42d3-951e-8dc2e6a23c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""2ca86ecd-b9f7-400e-b5b1-c73fb87476a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""18fe208d-6eb6-4560-a3ce-61c94cb28958"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5386ff2b-2b8b-43df-b7de-ab24ca70d203"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""5b81eefd-1adb-4f09-9aed-215c15399fc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""bd93f835-1dab-4b29-b2b0-3636779b2763"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""a177a164-12fe-4db3-9c34-b006e4d4d4db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Button"",
                    ""id"": ""8217f539-996e-4985-99a1-b95b40a3c709"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""803080f6-3937-48ae-a9af-583810ffa67b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""ed950634-2b63-4cce-b07c-3cddf8c6b6e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7f1a1d3b-d897-44e9-987e-86f2571157b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""971358f8-2f35-47e5-b342-3e7a6fdcf41c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eab16816-95ed-4efe-a225-6d5f56049df5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fffd2263-8d39-45f0-9b8a-e3336f40b187"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""840caca5-7f1c-4fb2-beb6-fd5914cd1006"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""68985e1d-465c-451f-b157-404e100fb10a"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57540360-59d0-4ff5-8fd6-a2d739975719"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd2a6d6e-bfca-411a-b4e8-f55f2b13a8a0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold(duration=0.6)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e383577c-c97b-417d-83fc-5ab87bf4d5b8"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e0bc9f8-5652-4413-a18c-8bcced597492"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b688ef3-abb7-4473-8a19-5aacac79277a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fadaf510-ae00-468b-901e-67721782c818"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""573b895a-b052-4ef6-abd6-fc312fd9b8cc"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""441a3202-ee25-4871-932f-7ee45b0688a3"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_Movement = m_PlayerInput.FindAction("Movement", throwIfNotFound: true);
        m_PlayerInput_Shoot = m_PlayerInput.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerInput_Crouch = m_PlayerInput.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerInput_Jump = m_PlayerInput.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInput_Sprint = m_PlayerInput.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerInput_Throw = m_PlayerInput.FindAction("Throw", throwIfNotFound: true);
        m_PlayerInput_Slide = m_PlayerInput.FindAction("Slide", throwIfNotFound: true);
        m_PlayerInput_Climb = m_PlayerInput.FindAction("Climb", throwIfNotFound: true);
        m_PlayerInput_Roll = m_PlayerInput.FindAction("Roll", throwIfNotFound: true);
        m_PlayerInput_Attack = m_PlayerInput.FindAction("Attack", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_Movement;
    private readonly InputAction m_PlayerInput_Shoot;
    private readonly InputAction m_PlayerInput_Crouch;
    private readonly InputAction m_PlayerInput_Jump;
    private readonly InputAction m_PlayerInput_Sprint;
    private readonly InputAction m_PlayerInput_Throw;
    private readonly InputAction m_PlayerInput_Slide;
    private readonly InputAction m_PlayerInput_Climb;
    private readonly InputAction m_PlayerInput_Roll;
    private readonly InputAction m_PlayerInput_Attack;
    public struct PlayerInputActions
    {
        private @GameInput m_Wrapper;
        public PlayerInputActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerInput_Movement;
        public InputAction @Shoot => m_Wrapper.m_PlayerInput_Shoot;
        public InputAction @Crouch => m_Wrapper.m_PlayerInput_Crouch;
        public InputAction @Jump => m_Wrapper.m_PlayerInput_Jump;
        public InputAction @Sprint => m_Wrapper.m_PlayerInput_Sprint;
        public InputAction @Throw => m_Wrapper.m_PlayerInput_Throw;
        public InputAction @Slide => m_Wrapper.m_PlayerInput_Slide;
        public InputAction @Climb => m_Wrapper.m_PlayerInput_Climb;
        public InputAction @Roll => m_Wrapper.m_PlayerInput_Roll;
        public InputAction @Attack => m_Wrapper.m_PlayerInput_Attack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Shoot.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnShoot;
                @Crouch.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnCrouch;
                @Jump.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSprint;
                @Throw.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnThrow;
                @Slide.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSlide;
                @Climb.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnClimb;
                @Climb.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnClimb;
                @Climb.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnClimb;
                @Roll.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRoll;
                @Attack.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
                @Climb.started += instance.OnClimb;
                @Climb.performed += instance.OnClimb;
                @Climb.canceled += instance.OnClimb;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerInputActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnClimb(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
