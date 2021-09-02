using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechLayout : LayoutBase
{
    [SerializeField]
    private Button buttonClose = null;
    [SerializeField]
    private Button buttonUpgrade = null;
    [SerializeField]
    private Button buttonCancel = null;

    public TechNodeButton CurrentSelectedNode
    {
        get;
        private set;
    }

    [SerializeField]
    private Image imageTechIcon = null;
    private Sprite defaultTechIcon = null;
    [SerializeField]
    private Text techDescribeText = null;


    private ObjectSoundManager soundManager = null;
    [Header("Audios")]
    [SerializeField]
    private AudioClip clickAudio = null;
    

    public void SetCurrentNode(TechNodeButton button)
    {
        if (button)
        {
            CurrentSelectedNode = button;
            imageTechIcon.sprite  = button.TechSymbolImage;
            techDescribeText.text = button.TechDescribe;
            techDescribeText.gameObject.SetActive(true);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if(buttonClose)
        {
            SetVisible(false);
            buttonClose.onClick.AddListener(CloseWindow);
            buttonClose.onClick.AddListener(PlayClickSound);
        }
        if (buttonUpgrade)
        {
            buttonUpgrade.onClick.AddListener(UpgradeTech);
            buttonUpgrade.onClick.AddListener(PlayClickSound);
        }
        if(buttonCancel)
        {
            buttonCancel.onClick.AddListener(ClearTechDesc);
            buttonCancel.onClick.AddListener(CancelResearching);
            buttonCancel.onClick.AddListener(PlayClickSound);
        }
        soundManager = GetComponent<ObjectSoundManager>();
        defaultTechIcon = imageTechIcon.sprite;

       
    }

    private void UpgradeTech()
    {
        if (!CurrentSelectedNode)
            return;
        CurrentSelectedNode.Research();
    }

    private void CancelResearching()
    {
        if (!CurrentSelectedNode)
            return;
        CurrentSelectedNode.CancelResearchingAnimation();
        TechTree.Instance.CancelCurretnResearching();
    }

    private void ClearTechDesc()
    {
        if (CurrentSelectedNode)
        {
            CurrentSelectedNode = null;
        }
        if(techDescribeText)
        {
            techDescribeText.gameObject.SetActive(false);
            techDescribeText.text = null;
        }
        if(imageTechIcon)
        {
            imageTechIcon.sprite = defaultTechIcon;
        }
    }

    private void CloseWindow()
    {
        SetVisible(false);
    }

    public void PlayClickSound()
    {
        soundManager.ForcePlay(clickAudio);
    }
}
