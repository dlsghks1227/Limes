// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4c570858-4516-49ed-aa3e-e4fb3d108242"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""85cdd82d-ff2c-4c51-8f4c-a8c500d5cd99"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mode Change"",
                    ""type"": ""Button"",
                    ""id"": ""9aca9bbb-928d-422a-8dda-9539a81f425e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""859038d8-95e7-4962-9ba5-9479a4a7d637"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mode Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""872f0981-675e-428f-914f-c34034200985"",
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
                    ""id"": ""46e31769-e480-4322-b3d5-068a30fca813"",
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
                    ""id"": ""800617c7-03b7-43fd-b81e-3831278b27ad"",
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
                    ""id"": ""97d0396e-227e-4325-b18a-4ddf96995d57"",
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
                    ""id"": ""f606069e-3e62-4fbc-843e-b0166a74a015"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""CameraMove"",
            ""id"": ""83b8b14d-d9b8-438b-85af-3f1f5b0578b0"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Value"",
                    ""id"": ""f5729265-ce5e-4c45-aa88-fff53097966c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""fb400bf2-892d-4c0d-9930-57cbe2d46a7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""cb7d2541-0f23-40db-9c15-f684c8675474"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""86c953d2-1a79-4184-a3b0-82f5761434ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7b91d8a1-4810-4257-b8d6-b6d3abb8f772"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdaeba7f-a96e-4b47-aea0-d7106e68c341"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cfea738-c7f4-4375-9c3b-25da0f9e3618"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e02fb39-21ff-47f1-99df-a6840c8adbed"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainState"",
            ""id"": ""733b5075-56db-4893-a5d9-395fad9f79bf"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5c33b102-0c93-4ac5-80be-4a8955f2aa69"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildTab"",
                    ""type"": ""Button"",
                    ""id"": ""89c83e1e-1530-4bca-8437-e45522556fcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TechTab"",
                    ""type"": ""Button"",
                    ""id"": ""5481a146-6416-43e9-b8e0-e8c5a4f00804"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d220d77e-edf9-4128-ac47-937ad68165ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerStatus"",
                    ""type"": ""Button"",
                    ""id"": ""40452cec-11d8-4603-ab5a-506d1c392fe3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BarrackUnit"",
                    ""type"": ""Button"",
                    ""id"": ""a415637b-1417-4c01-be3e-da56a4eb6dc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AirDropUnit"",
                    ""type"": ""Button"",
                    ""id"": ""cbf4fb24-d687-4749-a9be-cb9f1b541178"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ExchangeTab"",
                    ""type"": ""Button"",
                    ""id"": ""4d50b645-aee6-4621-8e0f-7841c24cdc79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""89137994-1982-4cea-858e-6d46b31e6df0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8eecd045-35cd-44a1-9178-3f01887271e0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""879a07a6-af70-43b6-890c-a6115be408b6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9bb2d145-638c-42f9-ac14-955bcc3893e7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ab6d470c-62bc-4d89-9e4a-26680f8c31ec"",
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
                    ""id"": ""7b20ea66-8860-433e-94a8-bf1008e0db80"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BuildTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4c12b20-1ac5-4fac-bda4-480b0804c517"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TechTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6246dcf3-ca9b-45e2-8740-372109bac75d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f3330a0-7ac6-4d9a-b018-8da140fd1576"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerStatus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da8df388-e962-4c9e-a1b7-29d2ed6ec76e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BarrackUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9384cd1d-7e9b-4b27-8d00-284fdc4b49c2"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDropUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4c61799-4dde-48fc-8108-e36a8cc7637b"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExchangeTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuildState"",
            ""id"": ""b3ba6261-212c-4bad-b184-201a211cef2e"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""86499303-bbc1-4f1f-b320-b5839949a401"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pick"",
                    ""type"": ""Button"",
                    ""id"": ""197b7b49-eee8-4792-93eb-d65908b4d69d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Locate"",
                    ""type"": ""Button"",
                    ""id"": ""6f163396-f4e5-43d6-a188-e0a6e0426342"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""841a28b8-0ca2-4525-88c1-558ca38cbef1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Button"",
                    ""id"": ""3173f19a-267b-43b2-9878-90d36a6c67ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""32136e1f-7626-40f2-adc3-a5a9dff8fd16"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5a5f926-4f58-4b20-884a-95b20218eec0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""641195ee-4337-43c5-8368-ab2ba7f720a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3e59a59-bb59-4ef5-b9f9-36090b358404"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccd5c71b-d89b-49b4-816a-f75c75283d70"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TechState"",
            ""id"": ""9d0ea950-d54e-461f-b396-4877659c8c40"",
            ""actions"": [
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""71b7e542-d774-4644-8bbe-90242038688b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a0a57b37-7459-4e45-a94b-967de2fbcf1d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseState"",
            ""id"": ""884f76ec-a271-43c5-ba33-a7e00b3b7328"",
            ""actions"": [
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""f6b0d800-5205-4b57-a1aa-390370d56951"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d57cc1e-6f87-4d66-9f52-bb3b1c72bff1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UnitState"",
            ""id"": ""68540934-79e2-4682-b297-b08ed594f82b"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""531c901a-3593-473a-ad88-8a638d2f35f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3d2cd662-4a24-4ced-ba0d-a5a8de29789f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SkillState"",
            ""id"": ""000906c1-e4d5-4550-aa63-a47ffcfa5412"",
            ""actions"": [
                {
                    ""name"": ""GetSkillPos"",
                    ""type"": ""Button"",
                    ""id"": ""05afc679-4429-496a-88ec-06b2c070f3c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2173af03-fc16-40dd-a773-cd7f72df3ae4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetSkillPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_ModeChange = m_Player.FindAction("Mode Change", throwIfNotFound: true);
        // CameraMove
        m_CameraMove = asset.FindActionMap("CameraMove", throwIfNotFound: true);
        m_CameraMove_Left = m_CameraMove.FindAction("Left", throwIfNotFound: true);
        m_CameraMove_Right = m_CameraMove.FindAction("Right", throwIfNotFound: true);
        m_CameraMove_Up = m_CameraMove.FindAction("Up", throwIfNotFound: true);
        m_CameraMove_Down = m_CameraMove.FindAction("Down", throwIfNotFound: true);
        // MainState
        m_MainState = asset.FindActionMap("MainState", throwIfNotFound: true);
        m_MainState_Move = m_MainState.FindAction("Move", throwIfNotFound: true);
        m_MainState_BuildTab = m_MainState.FindAction("BuildTab", throwIfNotFound: true);
        m_MainState_TechTab = m_MainState.FindAction("TechTab", throwIfNotFound: true);
        m_MainState_Pause = m_MainState.FindAction("Pause", throwIfNotFound: true);
        m_MainState_PlayerStatus = m_MainState.FindAction("PlayerStatus", throwIfNotFound: true);
        m_MainState_BarrackUnit = m_MainState.FindAction("BarrackUnit", throwIfNotFound: true);
        m_MainState_AirDropUnit = m_MainState.FindAction("AirDropUnit", throwIfNotFound: true);
        m_MainState_ExchangeTab = m_MainState.FindAction("ExchangeTab", throwIfNotFound: true);
        // BuildState
        m_BuildState = asset.FindActionMap("BuildState", throwIfNotFound: true);
        m_BuildState_Rotate = m_BuildState.FindAction("Rotate", throwIfNotFound: true);
        m_BuildState_Pick = m_BuildState.FindAction("Pick", throwIfNotFound: true);
        m_BuildState_Locate = m_BuildState.FindAction("Locate", throwIfNotFound: true);
        m_BuildState_Esc = m_BuildState.FindAction("Esc", throwIfNotFound: true);
        m_BuildState_Build = m_BuildState.FindAction("Build", throwIfNotFound: true);
        // TechState
        m_TechState = asset.FindActionMap("TechState", throwIfNotFound: true);
        m_TechState_Esc = m_TechState.FindAction("Esc", throwIfNotFound: true);
        // PauseState
        m_PauseState = asset.FindActionMap("PauseState", throwIfNotFound: true);
        m_PauseState_Esc = m_PauseState.FindAction("Esc", throwIfNotFound: true);
        // UnitState
        m_UnitState = asset.FindActionMap("UnitState", throwIfNotFound: true);
        m_UnitState_Newaction = m_UnitState.FindAction("New action", throwIfNotFound: true);
        // SkillState
        m_SkillState = asset.FindActionMap("SkillState", throwIfNotFound: true);
        m_SkillState_GetSkillPos = m_SkillState.FindAction("GetSkillPos", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_ModeChange;
    public struct PlayerActions
    {
        private @PlayerControl m_Wrapper;
        public PlayerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @ModeChange => m_Wrapper.m_Player_ModeChange;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @ModeChange.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnModeChange;
                @ModeChange.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnModeChange;
                @ModeChange.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnModeChange;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @ModeChange.started += instance.OnModeChange;
                @ModeChange.performed += instance.OnModeChange;
                @ModeChange.canceled += instance.OnModeChange;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // CameraMove
    private readonly InputActionMap m_CameraMove;
    private ICameraMoveActions m_CameraMoveActionsCallbackInterface;
    private readonly InputAction m_CameraMove_Left;
    private readonly InputAction m_CameraMove_Right;
    private readonly InputAction m_CameraMove_Up;
    private readonly InputAction m_CameraMove_Down;
    public struct CameraMoveActions
    {
        private @PlayerControl m_Wrapper;
        public CameraMoveActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_CameraMove_Left;
        public InputAction @Right => m_Wrapper.m_CameraMove_Right;
        public InputAction @Up => m_Wrapper.m_CameraMove_Up;
        public InputAction @Down => m_Wrapper.m_CameraMove_Down;
        public InputActionMap Get() { return m_Wrapper.m_CameraMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMoveActions set) { return set.Get(); }
        public void SetCallbacks(ICameraMoveActions instance)
        {
            if (m_Wrapper.m_CameraMoveActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnRight;
                @Up.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnDown;
            }
            m_Wrapper.m_CameraMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
            }
        }
    }
    public CameraMoveActions @CameraMove => new CameraMoveActions(this);

    // MainState
    private readonly InputActionMap m_MainState;
    private IMainStateActions m_MainStateActionsCallbackInterface;
    private readonly InputAction m_MainState_Move;
    private readonly InputAction m_MainState_BuildTab;
    private readonly InputAction m_MainState_TechTab;
    private readonly InputAction m_MainState_Pause;
    private readonly InputAction m_MainState_PlayerStatus;
    private readonly InputAction m_MainState_BarrackUnit;
    private readonly InputAction m_MainState_AirDropUnit;
    private readonly InputAction m_MainState_ExchangeTab;
    public struct MainStateActions
    {
        private @PlayerControl m_Wrapper;
        public MainStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MainState_Move;
        public InputAction @BuildTab => m_Wrapper.m_MainState_BuildTab;
        public InputAction @TechTab => m_Wrapper.m_MainState_TechTab;
        public InputAction @Pause => m_Wrapper.m_MainState_Pause;
        public InputAction @PlayerStatus => m_Wrapper.m_MainState_PlayerStatus;
        public InputAction @BarrackUnit => m_Wrapper.m_MainState_BarrackUnit;
        public InputAction @AirDropUnit => m_Wrapper.m_MainState_AirDropUnit;
        public InputAction @ExchangeTab => m_Wrapper.m_MainState_ExchangeTab;
        public InputActionMap Get() { return m_Wrapper.m_MainState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainStateActions set) { return set.Get(); }
        public void SetCallbacks(IMainStateActions instance)
        {
            if (m_Wrapper.m_MainStateActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnMove;
                @BuildTab.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBuildTab;
                @BuildTab.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBuildTab;
                @BuildTab.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBuildTab;
                @TechTab.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnTechTab;
                @TechTab.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnTechTab;
                @TechTab.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnTechTab;
                @Pause.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPause;
                @PlayerStatus.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPlayerStatus;
                @PlayerStatus.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPlayerStatus;
                @PlayerStatus.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnPlayerStatus;
                @BarrackUnit.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBarrackUnit;
                @BarrackUnit.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBarrackUnit;
                @BarrackUnit.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnBarrackUnit;
                @AirDropUnit.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnAirDropUnit;
                @AirDropUnit.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnAirDropUnit;
                @AirDropUnit.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnAirDropUnit;
                @ExchangeTab.started -= m_Wrapper.m_MainStateActionsCallbackInterface.OnExchangeTab;
                @ExchangeTab.performed -= m_Wrapper.m_MainStateActionsCallbackInterface.OnExchangeTab;
                @ExchangeTab.canceled -= m_Wrapper.m_MainStateActionsCallbackInterface.OnExchangeTab;
            }
            m_Wrapper.m_MainStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @BuildTab.started += instance.OnBuildTab;
                @BuildTab.performed += instance.OnBuildTab;
                @BuildTab.canceled += instance.OnBuildTab;
                @TechTab.started += instance.OnTechTab;
                @TechTab.performed += instance.OnTechTab;
                @TechTab.canceled += instance.OnTechTab;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @PlayerStatus.started += instance.OnPlayerStatus;
                @PlayerStatus.performed += instance.OnPlayerStatus;
                @PlayerStatus.canceled += instance.OnPlayerStatus;
                @BarrackUnit.started += instance.OnBarrackUnit;
                @BarrackUnit.performed += instance.OnBarrackUnit;
                @BarrackUnit.canceled += instance.OnBarrackUnit;
                @AirDropUnit.started += instance.OnAirDropUnit;
                @AirDropUnit.performed += instance.OnAirDropUnit;
                @AirDropUnit.canceled += instance.OnAirDropUnit;
                @ExchangeTab.started += instance.OnExchangeTab;
                @ExchangeTab.performed += instance.OnExchangeTab;
                @ExchangeTab.canceled += instance.OnExchangeTab;
            }
        }
    }
    public MainStateActions @MainState => new MainStateActions(this);

    // BuildState
    private readonly InputActionMap m_BuildState;
    private IBuildStateActions m_BuildStateActionsCallbackInterface;
    private readonly InputAction m_BuildState_Rotate;
    private readonly InputAction m_BuildState_Pick;
    private readonly InputAction m_BuildState_Locate;
    private readonly InputAction m_BuildState_Esc;
    private readonly InputAction m_BuildState_Build;
    public struct BuildStateActions
    {
        private @PlayerControl m_Wrapper;
        public BuildStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_BuildState_Rotate;
        public InputAction @Pick => m_Wrapper.m_BuildState_Pick;
        public InputAction @Locate => m_Wrapper.m_BuildState_Locate;
        public InputAction @Esc => m_Wrapper.m_BuildState_Esc;
        public InputAction @Build => m_Wrapper.m_BuildState_Build;
        public InputActionMap Get() { return m_Wrapper.m_BuildState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildStateActions set) { return set.Get(); }
        public void SetCallbacks(IBuildStateActions instance)
        {
            if (m_Wrapper.m_BuildStateActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnRotate;
                @Pick.started -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnPick;
                @Pick.performed -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnPick;
                @Pick.canceled -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnPick;
                @Locate.started -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnLocate;
                @Locate.performed -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnLocate;
                @Locate.canceled -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnLocate;
                @Esc.started -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnEsc;
                @Build.started -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnBuild;
                @Build.performed -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnBuild;
                @Build.canceled -= m_Wrapper.m_BuildStateActionsCallbackInterface.OnBuild;
            }
            m_Wrapper.m_BuildStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Pick.started += instance.OnPick;
                @Pick.performed += instance.OnPick;
                @Pick.canceled += instance.OnPick;
                @Locate.started += instance.OnLocate;
                @Locate.performed += instance.OnLocate;
                @Locate.canceled += instance.OnLocate;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
                @Build.started += instance.OnBuild;
                @Build.performed += instance.OnBuild;
                @Build.canceled += instance.OnBuild;
            }
        }
    }
    public BuildStateActions @BuildState => new BuildStateActions(this);

    // TechState
    private readonly InputActionMap m_TechState;
    private ITechStateActions m_TechStateActionsCallbackInterface;
    private readonly InputAction m_TechState_Esc;
    public struct TechStateActions
    {
        private @PlayerControl m_Wrapper;
        public TechStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Esc => m_Wrapper.m_TechState_Esc;
        public InputActionMap Get() { return m_Wrapper.m_TechState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TechStateActions set) { return set.Get(); }
        public void SetCallbacks(ITechStateActions instance)
        {
            if (m_Wrapper.m_TechStateActionsCallbackInterface != null)
            {
                @Esc.started -= m_Wrapper.m_TechStateActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_TechStateActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_TechStateActionsCallbackInterface.OnEsc;
            }
            m_Wrapper.m_TechStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
            }
        }
    }
    public TechStateActions @TechState => new TechStateActions(this);

    // PauseState
    private readonly InputActionMap m_PauseState;
    private IPauseStateActions m_PauseStateActionsCallbackInterface;
    private readonly InputAction m_PauseState_Esc;
    public struct PauseStateActions
    {
        private @PlayerControl m_Wrapper;
        public PauseStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Esc => m_Wrapper.m_PauseState_Esc;
        public InputActionMap Get() { return m_Wrapper.m_PauseState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseStateActions set) { return set.Get(); }
        public void SetCallbacks(IPauseStateActions instance)
        {
            if (m_Wrapper.m_PauseStateActionsCallbackInterface != null)
            {
                @Esc.started -= m_Wrapper.m_PauseStateActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_PauseStateActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_PauseStateActionsCallbackInterface.OnEsc;
            }
            m_Wrapper.m_PauseStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
            }
        }
    }
    public PauseStateActions @PauseState => new PauseStateActions(this);

    // UnitState
    private readonly InputActionMap m_UnitState;
    private IUnitStateActions m_UnitStateActionsCallbackInterface;
    private readonly InputAction m_UnitState_Newaction;
    public struct UnitStateActions
    {
        private @PlayerControl m_Wrapper;
        public UnitStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UnitState_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_UnitState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UnitStateActions set) { return set.Get(); }
        public void SetCallbacks(IUnitStateActions instance)
        {
            if (m_Wrapper.m_UnitStateActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UnitStateActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UnitStateActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UnitStateActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_UnitStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public UnitStateActions @UnitState => new UnitStateActions(this);

    // SkillState
    private readonly InputActionMap m_SkillState;
    private ISkillStateActions m_SkillStateActionsCallbackInterface;
    private readonly InputAction m_SkillState_GetSkillPos;
    public struct SkillStateActions
    {
        private @PlayerControl m_Wrapper;
        public SkillStateActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @GetSkillPos => m_Wrapper.m_SkillState_GetSkillPos;
        public InputActionMap Get() { return m_Wrapper.m_SkillState; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkillStateActions set) { return set.Get(); }
        public void SetCallbacks(ISkillStateActions instance)
        {
            if (m_Wrapper.m_SkillStateActionsCallbackInterface != null)
            {
                @GetSkillPos.started -= m_Wrapper.m_SkillStateActionsCallbackInterface.OnGetSkillPos;
                @GetSkillPos.performed -= m_Wrapper.m_SkillStateActionsCallbackInterface.OnGetSkillPos;
                @GetSkillPos.canceled -= m_Wrapper.m_SkillStateActionsCallbackInterface.OnGetSkillPos;
            }
            m_Wrapper.m_SkillStateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GetSkillPos.started += instance.OnGetSkillPos;
                @GetSkillPos.performed += instance.OnGetSkillPos;
                @GetSkillPos.canceled += instance.OnGetSkillPos;
            }
        }
    }
    public SkillStateActions @SkillState => new SkillStateActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnModeChange(InputAction.CallbackContext context);
    }
    public interface ICameraMoveActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
    }
    public interface IMainStateActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnBuildTab(InputAction.CallbackContext context);
        void OnTechTab(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnPlayerStatus(InputAction.CallbackContext context);
        void OnBarrackUnit(InputAction.CallbackContext context);
        void OnAirDropUnit(InputAction.CallbackContext context);
        void OnExchangeTab(InputAction.CallbackContext context);
    }
    public interface IBuildStateActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnPick(InputAction.CallbackContext context);
        void OnLocate(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
    }
    public interface ITechStateActions
    {
        void OnEsc(InputAction.CallbackContext context);
    }
    public interface IPauseStateActions
    {
        void OnEsc(InputAction.CallbackContext context);
    }
    public interface IUnitStateActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface ISkillStateActions
    {
        void OnGetSkillPos(InputAction.CallbackContext context);
    }
}
