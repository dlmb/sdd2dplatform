// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputAction.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerInputAction : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0d220c4f-fb5a-41ab-bf5e-571ce6016619"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ccb65b4-cde0-4333-b88c-ea433241c712"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""run"",
                    ""type"": ""PassThrough"",
                    ""id"": ""61e40ca2-81b5-4626-81dc-7c9c19005ab0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""squat"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f830859f-a911-4553-b758-bf2b418bd9cc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5c4b32e4-79b2-4a23-94d0-4f42f059d887"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireDirection"",
                    ""type"": ""Value"",
                    ""id"": ""423fac63-c6e0-42d7-94e1-2f7158c11e82"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7657b1ed-2240-410d-aaf4-d4a7807e1e9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""b0a2f676-79c2-4dbb-b163-c0b9d7b617ed"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""773a9ed6-4d1e-4b48-8d77-6179c508a562"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyBoard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e11cb29f-1170-4361-b67c-c6b1860ed0a0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyBoard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""206c453d-86a2-47ca-80bc-5a9e4c35f7a1"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press,Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""KeyBoard&Mouse"",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2de8406-2697-4121-bfda-e20350e51d44"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyBoard&Mouse"",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ac8a7c9-466a-40df-87ee-763741992a31"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e64ce50-9345-4e05-a9f4-8d175d3e5613"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap(duration=0.5),SlowTap(duration=2.5)"",
                    ""processors"": """",
                    ""groups"": ""KeyBoard&Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d05b1bc-88dd-4ad7-8459-1e735f676dfb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press,Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": "";KeyBoard&Mouse"",
                    ""action"": ""squat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard&Mouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""KeyBoard&Mouse"",
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
        m_Player = asset.GetActionMap("Player");
        m_Player_Move = m_Player.GetAction("Move");
        m_Player_run = m_Player.GetAction("run");
        m_Player_squat = m_Player.GetAction("squat");
        m_Player_jump = m_Player.GetAction("jump");
        m_Player_FireDirection = m_Player.GetAction("FireDirection");
        m_Player_Shoot = m_Player.GetAction("Shoot");
    }

    ~PlayerInputAction()
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
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_run;
    private readonly InputAction m_Player_squat;
    private readonly InputAction m_Player_jump;
    private readonly InputAction m_Player_FireDirection;
    private readonly InputAction m_Player_Shoot;
    public struct PlayerActions
    {
        private PlayerInputAction m_Wrapper;
        public PlayerActions(PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @run => m_Wrapper.m_Player_run;
        public InputAction @squat => m_Wrapper.m_Player_squat;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @FireDirection => m_Wrapper.m_Player_FireDirection;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                squat.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                squat.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                squat.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                FireDirection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireDirection;
                FireDirection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireDirection;
                FireDirection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireDirection;
                Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                run.started += instance.OnRun;
                run.performed += instance.OnRun;
                run.canceled += instance.OnRun;
                squat.started += instance.OnSquat;
                squat.performed += instance.OnSquat;
                squat.canceled += instance.OnSquat;
                jump.started += instance.OnJump;
                jump.performed += instance.OnJump;
                jump.canceled += instance.OnJump;
                FireDirection.started += instance.OnFireDirection;
                FireDirection.performed += instance.OnFireDirection;
                FireDirection.canceled += instance.OnFireDirection;
                Shoot.started += instance.OnShoot;
                Shoot.performed += instance.OnShoot;
                Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyBoardMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardMouseScheme
    {
        get
        {
            if (m_KeyBoardMouseSchemeIndex == -1) m_KeyBoardMouseSchemeIndex = asset.GetControlSchemeIndex("KeyBoard&Mouse");
            return asset.controlSchemes[m_KeyBoardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnSquat(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFireDirection(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
