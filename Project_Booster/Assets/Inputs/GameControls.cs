// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Player
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f443b652-709c-499e-9048-324efefdb8ff"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""c1a20404-c155-4874-b55a-1826c2627c0f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""70dcc395-9911-4e57-b875-1e7a2361c20c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""489d1e36-21ed-42ad-906f-35eb85e0de30"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9378bef3-7b7d-4f24-9b90-b54e07431e2f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""8338065d-3904-41e1-a846-20501e11eff9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4ccb297a-58a3-4053-960d-b9fbc9a3e4c0"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""874bbca2-3456-4fc9-bcf4-40bb1d3e488a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""c80b2011-15b6-44a1-9178-81dacdc671cd"",
            ""actions"": [
                {
                    ""name"": ""RestartLevel"",
                    ""type"": ""Button"",
                    ""id"": ""6ba2492a-04b1-4bf5-8a18-2a9c95e641f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartGame"",
                    ""type"": ""Button"",
                    ""id"": ""61cde1c8-5109-47b3-849f-c38948143dd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ExitGame"",
                    ""type"": ""Button"",
                    ""id"": ""569ec919-5bfc-4093-88eb-1f4c5e62cb02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""42af2121-50ed-40bf-9902-b83e4ef139f3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4a4bb87-2d99-4d6e-bf90-b46556527a99"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9e12d4b-1c6d-4627-b64e-435592e94026"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitGame"",
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
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_HorizontalMovement = m_Player.FindAction("HorizontalMovement", throwIfNotFound: true);
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_RestartLevel = m_Menu.FindAction("RestartLevel", throwIfNotFound: true);
            m_Menu_StartGame = m_Menu.FindAction("StartGame", throwIfNotFound: true);
            m_Menu_ExitGame = m_Menu.FindAction("ExitGame", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_HorizontalMovement;
        public struct PlayerActions
        {
            private @GameControls m_Wrapper;
            public PlayerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @HorizontalMovement => m_Wrapper.m_Player_HorizontalMovement;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @HorizontalMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                    @HorizontalMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                    @HorizontalMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @HorizontalMovement.started += instance.OnHorizontalMovement;
                    @HorizontalMovement.performed += instance.OnHorizontalMovement;
                    @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private readonly InputAction m_Menu_RestartLevel;
        private readonly InputAction m_Menu_StartGame;
        private readonly InputAction m_Menu_ExitGame;
        public struct MenuActions
        {
            private @GameControls m_Wrapper;
            public MenuActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @RestartLevel => m_Wrapper.m_Menu_RestartLevel;
            public InputAction @StartGame => m_Wrapper.m_Menu_StartGame;
            public InputAction @ExitGame => m_Wrapper.m_Menu_ExitGame;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    @RestartLevel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnRestartLevel;
                    @RestartLevel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnRestartLevel;
                    @RestartLevel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnRestartLevel;
                    @StartGame.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
                    @StartGame.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
                    @StartGame.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
                    @ExitGame.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnExitGame;
                    @ExitGame.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnExitGame;
                    @ExitGame.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnExitGame;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RestartLevel.started += instance.OnRestartLevel;
                    @RestartLevel.performed += instance.OnRestartLevel;
                    @RestartLevel.canceled += instance.OnRestartLevel;
                    @StartGame.started += instance.OnStartGame;
                    @StartGame.performed += instance.OnStartGame;
                    @StartGame.canceled += instance.OnStartGame;
                    @ExitGame.started += instance.OnExitGame;
                    @ExitGame.performed += instance.OnExitGame;
                    @ExitGame.canceled += instance.OnExitGame;
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnHorizontalMovement(InputAction.CallbackContext context);
        }
        public interface IMenuActions
        {
            void OnRestartLevel(InputAction.CallbackContext context);
            void OnStartGame(InputAction.CallbackContext context);
            void OnExitGame(InputAction.CallbackContext context);
        }
    }
}
