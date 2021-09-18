// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Camera/CameraInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CameraInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraInput"",
    ""maps"": [
        {
            ""name"": ""World"",
            ""id"": ""3ed5b0b5-e821-4ad4-a3d6-9a61bfcd2b3d"",
            ""actions"": [
                {
                    ""name"": ""OrbitActive"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8f0581ef-006b-4147-b6bc-7ba55cee3372"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c71e5971-142a-42d1-9c0e-8286a31db1aa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ba60ccc-c661-4c8a-a320-9d97b4622941"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OrbitActive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4191809e-8539-40a4-83ef-5437a5dac1fc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // World
        m_World = asset.FindActionMap("World", throwIfNotFound: true);
        m_World_OrbitActive = m_World.FindAction("OrbitActive", throwIfNotFound: true);
        m_World_Drag = m_World.FindAction("Drag", throwIfNotFound: true);
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

    // World
    private readonly InputActionMap m_World;
    private IWorldActions m_WorldActionsCallbackInterface;
    private readonly InputAction m_World_OrbitActive;
    private readonly InputAction m_World_Drag;
    public struct WorldActions
    {
        private @CameraInput m_Wrapper;
        public WorldActions(@CameraInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @OrbitActive => m_Wrapper.m_World_OrbitActive;
        public InputAction @Drag => m_Wrapper.m_World_Drag;
        public InputActionMap Get() { return m_Wrapper.m_World; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorldActions set) { return set.Get(); }
        public void SetCallbacks(IWorldActions instance)
        {
            if (m_Wrapper.m_WorldActionsCallbackInterface != null)
            {
                @OrbitActive.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnOrbitActive;
                @OrbitActive.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnOrbitActive;
                @OrbitActive.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnOrbitActive;
                @Drag.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnDrag;
            }
            m_Wrapper.m_WorldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OrbitActive.started += instance.OnOrbitActive;
                @OrbitActive.performed += instance.OnOrbitActive;
                @OrbitActive.canceled += instance.OnOrbitActive;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
            }
        }
    }
    public WorldActions @World => new WorldActions(this);
    public interface IWorldActions
    {
        void OnOrbitActive(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
    }
}
