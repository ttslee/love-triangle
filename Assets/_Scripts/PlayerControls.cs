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
            ""name"": ""Gameplay"",
            ""id"": ""ffb737e2-b5d5-4abe-bec5-1c96fd3db4ae"",
            ""actions"": [
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""9258e6ba-d262-4a95-8783-61bc5a6404ee"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""T"",
                    ""type"": ""Button"",
                    ""id"": ""1e2b0127-4216-40e4-9472-3b1157664740"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""O"",
                    ""type"": ""Button"",
                    ""id"": ""5f74578c-f129-4411-95b4-e3fc3f041caf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""S"",
                    ""type"": ""Button"",
                    ""id"": ""a84c13e0-508f-472b-a00d-949859b58e3e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8776c676-73a7-4cb2-8043-93ca95289906"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
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
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
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
                    ""path"": ""<DualShockGamepad>/buttonEast"",
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
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""S"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_X = m_Gameplay.FindAction("X", throwIfNotFound: true);
        m_Gameplay_T = m_Gameplay.FindAction("T", throwIfNotFound: true);
        m_Gameplay_O = m_Gameplay.FindAction("O", throwIfNotFound: true);
        m_Gameplay_S = m_Gameplay.FindAction("S", throwIfNotFound: true);
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
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @X => m_Wrapper.m_Gameplay_X;
        public InputAction @T => m_Wrapper.m_Gameplay_T;
        public InputAction @O => m_Wrapper.m_Gameplay_O;
        public InputAction @S => m_Wrapper.m_Gameplay_S;
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
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnX(InputAction.CallbackContext context);
        void OnT(InputAction.CallbackContext context);
        void OnO(InputAction.CallbackContext context);
        void OnS(InputAction.CallbackContext context);
    }
}
