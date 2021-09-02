using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLayout : LayoutBase
{
    [SerializeField]
    private Button buttonMainMenu = null;
    [SerializeField]
    private Button buttonResume = null;
    [SerializeField]
    private Button buttonExit = null;

    [SerializeField]
    private PlayerInputManager PlayerInputManager = null;

    private void Awake()
    {
        // 메인 메뉴 버튼
        if(buttonMainMenu)
        {
            buttonMainMenu.onClick.AddListener(LoadMainMenu);
        }
        // 재개하기 버튼
        if(buttonResume)
        {
            buttonResume.onClick.AddListener(Resume);
        }
        // 종료하기 버튼
        if(buttonExit)
        {
            buttonExit.onClick.AddListener(Exit);
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    private void Resume()
    {
        PlayerInputManager.ClickPauseLayout();
    }
    private void Exit()
    {
        Application.Quit();
    }
}
