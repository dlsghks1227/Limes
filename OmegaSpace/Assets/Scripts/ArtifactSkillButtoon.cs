using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor;


class ArtifactSkillButtoon : MonoBehaviour
{
    private ArtifactShopLayout skillMerchant;
    [SerializeField]
    private ActiveSkillBase activeArtifact = null;
    [SerializeField]
    private PassiveSkillBase passiveArtifact = null;
    [SerializeField]
    private Text skillNameText = null;
    public void OnClick()
    {
        if (skillMerchant == null)
            skillMerchant = FindObjectOfType<ArtifactShopLayout>() as ArtifactShopLayout;

        if (activeArtifact)
            skillMerchant.SelectSkill(activeArtifact);
        else if (passiveArtifact)
            skillMerchant.SelectSkill(passiveArtifact);
    }

    private void Awake()
    {
        if (activeArtifact)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = activeArtifact.Icon;
            if (skillNameText)
                skillNameText.text = activeArtifact.SkillName;
        }
        else if (passiveArtifact)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = passiveArtifact.Icon;
            if (skillNameText)
                skillNameText.text = passiveArtifact.SkillName;
        }
        else if (!passiveArtifact)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

