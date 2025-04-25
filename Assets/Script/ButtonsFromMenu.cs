using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonsFromMenu : MonoBehaviour
{
    public Button StartButton;

    void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(StartGame);
        } 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void Exit()
    {
        Debug.Log("Game is exit");
        Application.Quit();
    }
}
