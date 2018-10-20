using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour {


    public void Awake()
    {
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.SetString("boss", "dragon");
    }


    public void quitButton()
    {
        Application.Quit();
    }

    public void startButton()
    {
        if (allInputsAreDone())
        {
            SceneManager.LoadScene("BossFight");
        }
    }

    public void menuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void updateColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb;
    }

    private bool allInputsAreDone()
    {
        return (!string.IsNullOrEmpty(PlayerPrefs.GetString("name")));
    }



}
