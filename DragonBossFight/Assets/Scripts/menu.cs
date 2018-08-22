using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour {

    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    public Color onColor;
    public Color offColor;


    private bool easy = false;
    private bool medium = false;
    private bool hard = false;

    public void Awake()
    {
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("difficulty");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.SetString("boss", "dragon");
    }


    public void quitButton()
    {
        Application.Quit();
    }

    public void onEasyButton()
    {
        easy = true;
        medium = false;
        hard = false;
        clearButtons();
        updateColor(easyButton, onColor);
        PlayerPrefs.SetInt("difficulty", 1);
        
    }
    public void onMediumButton()
    {
        easy = false;
        medium = true;
        hard = false;
        clearButtons();
        updateColor(mediumButton, onColor);
        PlayerPrefs.SetInt("difficulty", 2);

    }
    public void onHardButton()
    {
        easy = false;
        medium = false;
        hard = true;
        clearButtons();
        updateColor(hardButton, onColor);
        PlayerPrefs.SetInt("difficulty", 3);
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

    public void clearButtons()
    {
        updateColor(easyButton, offColor);
        updateColor(mediumButton, offColor);
        updateColor(hardButton, offColor);
    }

    private void updateColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb;
    }

    private bool allInputsAreDone()
    {
        return (PlayerPrefs.GetInt("difficulty") != 0 && !string.IsNullOrEmpty(PlayerPrefs.GetString("name")));
    }



}
