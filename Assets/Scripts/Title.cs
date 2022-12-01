using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private string currentStage = "";

    public Button loadBtn;

    private void Start()
    {
        currentStage = DataManager.instance.Load();
        if (currentStage == "")
        {
            loadBtn.interactable = false;
        }
    }

    public void BtnGameFirstStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void BtnGameLoadStart()
    {
        SceneManager.LoadScene(currentStage);
    }

    public void BtnGameExit()
    {
        Application.Quit();
    }
}
