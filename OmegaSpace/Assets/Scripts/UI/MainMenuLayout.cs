using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLayout : LayoutBase
{
    [SerializeField]
    private Button buttonStart = null;
    [SerializeField]
    private Button buttonResume = null;
    [SerializeField]
    private Button buttonExit = null;
    [SerializeField]
    private RectTransform nodeNotice = null;


    private void Awake()
    {
        // 시작 버튼
        if(buttonStart)
        {
            buttonStart.onClick.AddListener(StartGame);
        }
        // 재개 버튼
        if(buttonResume)
        {
            if(nodeNotice)
            {
                nodeNotice.gameObject.SetActive(false);
            }
            buttonResume.onClick.AddListener(PrintNonPrepareMessage);
        }
        // 나가기 버튼
        if (buttonExit)
        {
            buttonExit.onClick.AddListener(Exit);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SpaceScene");
    }

    private void PrintNonPrepareMessage()
    {
        if(nodeNotice == null)
        {
            return;
        }

        nodeNotice.gameObject.SetActive(true);
    }

    private void Exit()
    {
        Debug.Log("나가기");
        Application.Quit();
    }
}
