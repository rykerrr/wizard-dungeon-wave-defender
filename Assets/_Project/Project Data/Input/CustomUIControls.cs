// GENERATED AUTOMATICALLY FROM 'Assets/Input/CustomUIControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CustomUIControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CustomUIControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomUIControls"",
    ""maps"": [
        {
            ""name"": ""Inventory"",
            ""id"": ""d23c66fd-79eb-4525-b1b1-790c2771f6ef"",
            ""actions"": [
                {
                    ""name"": ""Scroll Through Hotbar Slots"",
                    ""type"": ""Value"",
                    ""id"": ""bc3dc4e3-a1c8-4cee-9424-d0274209ca32"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ff3f186-615c-4dc5-806c-98d52afea815"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K+M"",
                    ""action"": ""Scroll Through Hotbar Slots"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""K+M"",
            ""bindingGroup"": ""K+M"",
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
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_ScrollThroughHotbarSlots = m_Inventory.FindAction("Scroll Through Hotbar Slots", throwIfNotFound: true);
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

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_ScrollThroughHotbarSlots;
    public struct InventoryActions
    {
        private @CustomUIControls m_Wrapper;
        public InventoryActions(@CustomUIControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ScrollThroughHotbarSlots => m_Wrapper.m_Inventory_ScrollThroughHotbarSlots;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @ScrollThroughHotbarSlots.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnScrollThroughHotbarSlots;
                @ScrollThroughHotbarSlots.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnScrollThroughHotbarSlots;
                @ScrollThroughHotbarSlots.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnScrollThroughHotbarSlots;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ScrollThroughHotbarSlots.started += instance.OnScrollThroughHotbarSlots;
                @ScrollThroughHotbarSlots.performed += instance.OnScrollThroughHotbarSlots;
                @ScrollThroughHotbarSlots.canceled += instance.OnScrollThroughHotbarSlots;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    private int m_KMSchemeIndex = -1;
    public InputControlScheme KMScheme
    {
        get
        {
            if (m_KMSchemeIndex == -1) m_KMSchemeIndex = asset.FindControlSchemeIndex("K+M");
            return asset.controlSchemes[m_KMSchemeIndex];
        }
    }
    public interface IInventoryActions
    {
        void OnScrollThroughHotbarSlots(InputAction.CallbackContext context);
    }
}
