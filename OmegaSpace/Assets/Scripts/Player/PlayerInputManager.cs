using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputState : MonoBehaviour
{
    public virtual void OnEnterState() { enabled = true; }
    public virtual void OnExitState() { enabled = false; }
}

public class PlayerInputManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputPlayerControl = null;
    [SerializeField] private InputObjectImage buildImg = null;

    private InputBuildState buildState = null;
    private InputMainState mainState = null;
    private InputTechState techState = null;
    private InputPauseState pauseState = null;
    private InputUnitState unitState = null;
    private InputExchangeState exchangeState = null;
    private InputSkillState skillState = null;


    private InputAction buildButton = null;
    private InputAction techButton = null;
    private InputAction unitButton = null;
    private InputAction exchangeButton = null;
    private InputAction pauseButton = null;

    private InputAction closeButton = null;

    private CanvasGroup buildTab = null;
    private CanvasGroup pauseTab = null;
    private CanvasGroup techTab = null;
    private CanvasGroup playerStat = null;
    private CanvasGroup unitTab = null;
    private CanvasGroup exchangeTab = null;

    private PlayerInputState current = null;

    BuildingEntity buildingEntity;

    private void Awake()
    {
        if (inputPlayerControl == null)
        {
            Debug.LogError("BuildControl have not found");
            enabled = false;
            return;
        }

        mainState = GetComponent<InputMainState>();
        buildState = GetComponent<InputBuildState>();
        techState = GetComponent<InputTechState>();
        pauseState = GetComponent<InputPauseState>();
        unitState = GetComponent<InputUnitState>();
        exchangeState = GetComponent<InputExchangeState>();
        skillState = GetComponent<InputSkillState>();

        //mainState.InitializeInput(inputPlayerControl);
        buildState.InitializeInput(inputPlayerControl);
        techState.InitializeInput(inputPlayerControl);
        pauseState.InitializeInput(inputPlayerControl);
        unitState.InitializeInput(inputPlayerControl);
        skillState.InitializeInput(inputPlayerControl);

        InitializeInput();
        //InitializeImage();

        ChangeState(mainState);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<BuildingEntity>())
            {
                buildingEntity = hit.collider.gameObject.GetComponent<BuildingEntity>();
            }
        }
        if (buildingEntity)
        {
            ShowBuildingInfo(buildingEntity);
        }
    }

    private void InitializeInput()
    {
        buildButton = inputPlayerControl.FindActionMap("MainState").FindAction("BuildTab");
        techButton = inputPlayerControl.FindActionMap("MainState").FindAction("TechTab");
        unitButton = inputPlayerControl.FindActionMap("MainState").FindAction("BarrackUnit");
        exchangeButton = inputPlayerControl.FindActionMap("MainState").FindAction("ExchangeTab");
        pauseButton = inputPlayerControl.FindActionMap("MainState").FindAction("Pause");

        //closeButton = inputPlayerControl.FindActionMap("BuildState").FindAction("Esc");

        buildButton.performed += ctx => { ClickBuildLayout(); };
        techButton.performed += ctx => { ClickTechLayout(); };
        unitButton.performed += ctx => { ClickUnitLayout(); };
        exchangeButton.performed += ctx => { ClickExchangeLayout(); };
        pauseButton.performed += ctx => { ClickPauseLayout(); };

        //closeButton.performed += ctx => { ChangeState(mainState); };
    }

    public void ChangeState(PlayerInputState next)
    {
        if (current)
        {
            current.OnExitState();
        }
        current = next;
        current.OnEnterState();
    }

    public void ClickBuildLayout()
    {
        if (current == buildState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(buildState);
        }
    }

    public void ClickTechLayout()
    {
        if (current == techState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(techState);
        }
    }

    public void ClickPauseLayout()
    {
        if (current == pauseState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(pauseState);
        }
    }

    public void ClickUnitLayout()
    {
        if (current == unitState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(unitState);
        }
    }

    public void ClickExchangeLayout()
    {
        if (current == unitState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(exchangeState);
        }
    }

    public void ClickSkillLayout()
    {
        if (current == unitState)
        {
            ChangeState(mainState);
        }
        else
        {
            ChangeState(skillState);
        }
    }

    private void ShowBuildingInfo(BuildingEntity building)
    {
        GameObject.Find("Text_EntityName").GetComponent<Text>().text = GetName(building.name);

        GameObject.Find("Text_LV").GetComponent<Text>().text = building.Level.ToString();

        GameObject.Find("Text_HP").GetComponent<Text>().text = building.HealthPoint + " / " + building.HealthPointMax;

        GameObject.Find("Text_AP").GetComponent<Text>().text = building.ArmorPoint + " / " + building.ArmorPointMax;

        GameObject.Find("Text_TERR").GetComponent<Text>().text = building.Territory.ToString();

        buildImg.ShowSelectedObject(GetName(building.name));
    }

    private String GetName(String name)
    {
        int strinLength = name.Length;
        //exclude "(Clone)" from end of name
        return name.Substring(0, strinLength - 7);
    }

    public void OnEnable()
    {
        buildButton.Enable();
        techButton.Enable();
        unitButton.Enable();
        exchangeButton.Enable();
        pauseButton.Enable();
        //closeButton.Enable();
    }

    public void OnDisable()
    {
        buildButton.Disable();
        techButton.Disable();
        unitButton.Disable();
        exchangeButton.Disable();
        pauseButton.Disable();
        //closeButton.Disable();
    }
}