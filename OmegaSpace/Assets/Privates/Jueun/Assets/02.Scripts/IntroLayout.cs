using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLayout :MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("PerfectScene");
        Debug.Log("push");
    }
}
