// GENERATED AUTOMATICALLY FROM 'Assets/InputController/PlayerControls.inputactions'

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
            ""name"": ""Gameplay"",
            ""id"": ""ffb737e2-b5d5-4abe-bec5-1c96fd3db4ae"",
            ""actions"": [
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""9258e6ba-d262-4a95-8783-61bc5a6404ee"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""T"",
                    ""type"": ""Button"",
                    ""id"": ""1e2b0127-4216-40e4-9472-3b1157664740"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""O"",
                    ""type"": ""Button"",
                    ""id"": ""5f74578c-f129-4411-95b4-e3fc3f041caf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""S"",
                    ""type"": ""Button"",
                    ""id"": ""a84c13e0-508f-472b-a00d-949859b58e3e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""83dfc872-f2e2-4c04-a7c8-45ee923dad9e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""15825e90-6ca1-4cc6-817b-55ab8dc4682d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""025cb2d8-19df-461a-9495-5380f1babfb9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""1fbb3b13-7f6b-4034-89d1-8cde6d43f613"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""3403b7d0-8eed-4517-904f-6d59e41ec487"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LT"",
                    ""type"": ""Button"",
                    ""id"": ""c8561312-cbe7-4f83-88ff-9a9ced138821"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LB"",
                    ""type"": ""Button"",
                    ""id"": ""3f43583d-471a-42e1-82cf-4baaccccca2b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RT"",
                    ""type"": ""Button"",
                    ""id"": ""c5face2b-b758-477b-9fcd-4a284648f028"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""fc36a780-b714-4e46-94a7-9a2a44c03dae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LeftJoy"",
                    ""type"": ""Value"",
                    ""id"": ""f8cad211-d6c6-45f1-ba0d-6fd966146ebb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightJoy"",
                    ""type"": ""Button"",
                    ""id"": ""d6fef973-debf-4222-ad7f-46ac1b10d943"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8776c676-73a7-4cb2-8043-93ca95289906"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9f5afc1-d011-4512-b951-e4b9c2cf7e01"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""T"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bc202e5-d54e-494b-bb03-64603872a871"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""O"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ed7dddb-9cd2-49ea-a7e7-5c6f09c96938"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""S"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8eca136-61d0-4878-9c58-94e00bef52a6"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c276fcfe-237d-4636-9f8e-3f2abf53fef8"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""751ae167-ca03-4ebb-bbd0-3d441e747669"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d091e48-e6a3-4d33-83cf-8b4ecde11d98"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdb73d0d-f73f-4fb7-b2e6-8f9a6dac8828"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5334faeb-cc49-46ee-9ed4-ae45115a1e6a"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7060933c-72ea-4e40-9ee6-15cefb83032a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16ceffd2-0baa-48d2-98b5-8b0c30dbb2cc"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74b5ac8a-2d87-46e5-9647-a4b60f41911b"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01754fd3-b77c-476f-94d1-a7f357ab4d5b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftJoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18cac8ca-e734-4438-a8c0-ec694f03c517"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightJoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_X = m_Gameplay.FindAction("X", throwIfNotFound: true);
        m_Gameplay_T = m_Gameplay.FindAction("T", throwIfNotFound: true);
        m_Gameplay_O = m_Gameplay.FindAction("O", throwIfNotFound: true);
        m_Gameplay_S = m_Gameplay.FindAction("S", throwIfNotFound: true);
        m_Gameplay_Up = m_Gameplay.FindAction("Up", throwIfNotFound: true);
        m_Gameplay_Down = m_Gameplay.FindAction("Down", throwIfNotFound: true);
        m_Gameplay_Left = m_Gameplay.FindAction("Left", throwIfNotFound: true);
        m_Gameplay_Right = m_Gameplay.FindAction("Right", throwIfNotFound: true);
        m_Gameplay_Start = m_Gameplay.FindAction("Start", throwIfNotFound: true);
        m_Gameplay_LT = m_Gameplay.FindAction("LT", throwIfNotFound: true);
        m_Gameplay_LB = m_Gameplay.FindAction("LB", throwIfNotFound: true);
        m_Gameplay_RT = m_Gameplay.FindAction("RT", throwIfNotFound: true);
        m_Gameplay_RB = m_Gameplay.FindAction("RB", throwIfNotFound: true);
        m_Gameplay_LeftJoy = m_Gameplay.FindAction("LeftJoy", throwIfNotFound: true);
        m_Gameplay_RightJoy = m_Gameplay.FindAction("RightJoy", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_X;
    private readonly InputAction m_Gameplay_T;
    private readonly InputAction m_Gameplay_O;
    private readonly InputAction m_Gameplay_S;
    private readonly InputAction m_Gameplay_Up;
    private readonly InputAction m_Gameplay_Down;
    private readonly InputAction m_Gameplay_Left;
    private readonly InputAction m_Gameplay_Right;
    private readonly InputAction m_Gameplay_Start;
    private readonly InputAction m_Gameplay_LT;
    private readonly InputAction m_Gameplay_LB;
    private readonly InputAction m_Gameplay_RT;
    private readonly InputAction m_Gameplay_RB;
    private readonly InputAction m_Gameplay_LeftJoy;
    private readonly InputAction m_Gameplay_RightJoy;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @X => m_Wrapper.m_Gameplay_X;
        public InputAction @T => m_Wrapper.m_Gameplay_T;
        public InputAction @O => m_Wrapper.m_Gameplay_O;
        public InputAction @S => m_Wrapper.m_Gameplay_S;
        public InputAction @Up => m_Wrapper.m_Gameplay_Up;
        public InputAction @Down => m_Wrapper.m_Gameplay_Down;
        public InputAction @Left => m_Wrapper.m_Gameplay_Left;
        public InputAction @Right => m_Wrapper.m_Gameplay_Right;
        public InputAction @Start => m_Wrapper.m_Gameplay_Start;
        public InputAction @LT => m_Wrapper.m_Gameplay_LT;
        public InputAction @LB => m_Wrapper.m_Gameplay_LB;
        public InputAction @RT => m_Wrapper.m_Gameplay_RT;
        public InputAction @RB => m_Wrapper.m_Gameplay_RB;
        public InputAction @LeftJoy => m_Wrapper.m_Gameplay_LeftJoy;
        public InputAction @RightJoy => m_Wrapper.m_Gameplay_RightJoy;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @X.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @T.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnT;
                @T.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnT;
                @T.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnT;
                @O.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnO;
                @O.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnO;
                @O.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnO;
                @S.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnS;
                @S.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnS;
                @S.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnS;
                @Up.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Start.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @LT.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLT;
                @LT.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLT;
                @LT.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLT;
                @LB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLB;
                @LB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLB;
                @LB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLB;
                @RT.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRT;
                @RT.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRT;
                @RT.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRT;
                @RB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRB;
                @RB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRB;
                @RB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRB;
                @LeftJoy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftJoy;
                @LeftJoy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftJoy;
                @LeftJoy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftJoy;
                @RightJoy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightJoy;
                @RightJoy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightJoy;
                @RightJoy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightJoy;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @T.started += instance.OnT;
                @T.performed += instance.OnT;
                @T.canceled += instance.OnT;
                @O.started += instance.OnO;
                @O.performed += instance.OnO;
                @O.canceled += instance.OnO;
                @S.started += instance.OnS;
                @S.performed += instance.OnS;
                @S.canceled += instance.OnS;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @LT.started += instance.OnLT;
                @LT.performed += instance.OnLT;
                @LT.canceled += instance.OnLT;
                @LB.started += instance.OnLB;
                @LB.performed += instance.OnLB;
                @LB.canceled += instance.OnLB;
                @RT.started += instance.OnRT;
                @RT.performed += instance.OnRT;
                @RT.canceled += instance.OnRT;
                @RB.started += instance.OnRB;
                @RB.performed += instance.OnRB;
                @RB.canceled += instance.OnRB;
                @LeftJoy.started += instance.OnLeftJoy;
                @LeftJoy.performed += instance.OnLeftJoy;
                @LeftJoy.canceled += instance.OnLeftJoy;
                @RightJoy.started += instance.OnRightJoy;
                @RightJoy.performed += instance.OnRightJoy;
                @RightJoy.canceled += instance.OnRightJoy;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnX(InputAction.CallbackContext context);
        void OnT(InputAction.CallbackContext context);
        void OnO(InputAction.CallbackContext context);
        void OnS(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnLT(InputAction.CallbackContext context);
        void OnLB(InputAction.CallbackContext context);
        void OnRT(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnLeftJoy(InputAction.CallbackContext context);
        void OnRightJoy(InputAction.CallbackContext context);
    }
}
