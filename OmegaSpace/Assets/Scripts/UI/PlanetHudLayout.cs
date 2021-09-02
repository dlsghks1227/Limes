using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHudLayout : LayoutBase
{
    [Header("TopPanel")]
    [SerializeField]
    private Image imagePrevSkill = null;
    [SerializeField]
    private Button buttonCurrSkill = null;
    [SerializeField]
    private Image imageNextSkill = null;
    [SerializeField]
    private Button buttonViewChange = null;
    [SerializeField]
    private Button buttonViewArtifactShop = null;
    [SerializeField]
    private Image imageCoolTimeMask = null;
    [SerializeField]
    private Sprite spriteSkillNull = null;
    [SerializeField]
    private GameObject artifactShopLayout = null;

    [SerializeField]
    private GameObject techLayout = null;

    private ActiveSkillLayout skillLayoutManager = null;
    private ActiveSkillButton skillButtonEventListenr = null; 
    
    [Header("InfoPanel")]
    [SerializeField]
    private RawImage rawGridEntity = null;
    [SerializeField]
    private Text textEntityName = null;
    [SerializeField]
    private Text textLevel = null;
    [SerializeField]
    private Text textHP = null;
    [SerializeField]
    private Text textArmor = null;
    [SerializeField]
    private Text textTerr = null;

    [Header("ResourcePanel")]
    [SerializeField]
    private Image imageJewel = null;
    [SerializeField]
    private Text textJewelAmount = null;
    [SerializeField]
    private Text textDarkAmount = null;
    [SerializeField]
    private Text textEther = null;


    [Header("BottomPanel")]
    [SerializeField]
    private Button buttonMenu = null;
    [SerializeField]
    private Button buttonBuild = null;
    [SerializeField]
    private Button buttonTech = null;
    [SerializeField]
    private Button buttonUnit = null;

    [Header("Minimap")]
    [SerializeField]
    private RawImage rawMinimap = null;

    [Header("Center")]
    [SerializeField]
    private CanvasGroup warningPanel = null;

    [Header("GameObject")]
    [SerializeField]
    private PlayerStateManager  playerStateManager  = null;
    [SerializeField]
    private PlayerInputManager  playerInputManager  = null;

    

    protected override void Awake()
    {
        base.Awake();

        if(buttonViewChange)
        {
            buttonViewChange.onClick.AddListener(ViewChange);
        }

        if(buttonMenu)
        {
            buttonMenu.onClick.AddListener(OpenPauseLayout);
        }
        if(buttonBuild)
        {
            buttonBuild.onClick.AddListener(OpenBuildLayout);
        }
        if(buttonTech)
        {
            buttonTech.onClick.AddListener(OpenTechLayout);
        }
        if(buttonUnit)
        {
            buttonUnit.onClick.AddListener(OpenUnitLayout);
        }
        if (buttonCurrSkill)
        {
            buttonCurrSkill.onClick.AddListener(UseCurrentSkill);
        }
        if (buttonViewArtifactShop)
        {
            buttonViewArtifactShop.onClick.AddListener(ViewShop);
        }
        
        skillLayoutManager = GetComponent<ActiveSkillLayout>();
        if (skillLayoutManager)
        {
            skillLayoutManager.SetLayoutComponents(imagePrevSkill, buttonCurrSkill, imageNextSkill, imageCoolTimeMask, spriteSkillNull);
            skillButtonEventListenr = new ActiveSkillButton(skillLayoutManager);
        }
    }

    private void Update()
    {
        UpdateJewel();
        UpdateDarkMatter();
        UpdateEther();
        if(isWarning)
        {
            warningTime += Time.deltaTime;
            if (warningTime > 1.0f)
            {
                OffWarning();
            }
        }
    }

    #region TopPanel
    public void UseCurrentSkill()
    {
        skillButtonEventListenr.OnCiick();
    }

    public void ViewChange()
    {
        playerStateManager.ToggleState();
    }

    public void ViewShop()
    {
        artifactShopLayout.gameObject.SetActive(true);
    }

    #endregion

    #region InfoPanel
    public void SetEntityName(string name)
    {
        if(textEntityName == null)
        {
            return;
        }

        textEntityName.text = name;
    }

    public void SetEntityHP(int curHP, int maxHP)
    {
        if(textHP == null)
        {
            return;
        }

        textHP.text = curHP + " / " + maxHP;
    }

    public void SetEntityArmor(int armor)
    {
        if(textArmor == null)
        {
            return;
        }

        textArmor.text = armor.ToString();
    }

    public void SetEntityTerriory(int terr)
    {
        if(textTerr == null)
        {
            return;
        }

        textTerr.text = terr.ToString();
    }
    #endregion

    #region ResourcePanel
    public void SetJewelType()
    {
    }

    private void UpdateJewel()
    {
        if (textJewelAmount == null)
        {
            return;
        }
        textJewelAmount.text = PlayerDataManager.Instance.GetPlayerResourceOf(EResource.RES_IRON).resAmount.ToString();
    }

    private void UpdateDarkMatter()
    {
        if (textDarkAmount == null)
        {
            return;
        }
        textDarkAmount.text = PlayerDataManager.Instance.GetPlayerResourceOf(EResource.RES_GOLD).resAmount.ToString();
    }

    private void UpdateEther()
    {
        if (textEther == null)
        {
            return;
        }
        textEther.text = PlayerDataManager.Instance.GetPlayerResourceOf(EResource.RES_ETHER).resAmount.ToString();
    }
    #endregion

    #region Bottom
    private void OpenPauseLayout()
    {

    }

    private void OpenBuildLayout()
    {
        playerInputManager.ClickBuildLayout();
    }

    private void OpenTechLayout()
    {
        playerInputManager.ClickTechLayout();
        techLayout.SetActive(true);
    }

    private void OpenUnitLayout()
    {
        playerInputManager.ClickUnitLayout();
    }
    #endregion

    #region Minimap
    #endregion

    #region Warning
    private float warningTime = 0.0f;
    private bool isWarning = false;
    public void ShowWarning(string message)
    {
        warningPanel.transform.GetChild(1).GetComponent<Text>().text = message;
        warningPanel.alpha = 1;
        isWarning = true;
    }
    
    public void OffWarning()
    {
        isWarning = false;
        warningPanel.alpha = 0;
        warningPanel.transform.GetChild(1).GetComponent<Text>().text = "";
    }
    #endregion
}
