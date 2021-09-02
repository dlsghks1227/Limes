using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuLayout : MonoBehaviour
{
    [SerializeField]
    private GameObject PageMenu = null;
    [SerializeField]
    private GameObject PageSetting = null;
    [SerializeField]
    private GameObject MenuBoard = null; 
    [SerializeField]
    private Text txtTitle = null;

    private GameObject CurrentPage = null;

    // Start is called before the first frame update
    void Start()
    {
        MenuBoard.SetActive(false);
        PageSetting.SetActive(false);
    }
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void OnClickMenuButton()
    {
        PageMenu.SetActive(!(MenuBoard.activeSelf));
        PageSetting.SetActive(false);
        MenuBoard.SetActive(!(MenuBoard.activeSelf));
        CurrentPage = PageMenu;
    }
    public void OnclickDirectionButton()
    {
        if (CurrentPage == PageMenu)
        {
            CurrentPage.SetActive(false);
            PageSetting.SetActive(true);
            CurrentPage = PageSetting;
            txtTitle.text = "Setting";
        }
        else if(CurrentPage == PageSetting)
        {
            CurrentPage.SetActive(false);
            PageMenu.SetActive(true);
            CurrentPage = PageMenu;
            txtTitle.text = "Menu";
        }
    }
}
