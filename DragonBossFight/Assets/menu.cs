using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {

    private bool easy = false;
    private bool medium = false;
    private bool hard = false;


    public void quitButton()
    {
        Application.Quit();
    }

    public void easyButton()
    {
        easy = true;
        medium = false;
        hard = false;
    }
    public void mediumButton()
    {
        easy = false;
        medium = true;
        hard = false;
    }
    public void hardButton()
    {
        easy = false;
        medium = false;
        hard = true;
    }


}
