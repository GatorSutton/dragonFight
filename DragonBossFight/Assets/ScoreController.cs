using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public NotificationController Notification;

    private bool flawless;
    private Text scoreText;
    private int score;
    private int bonus;
    private int multiplier;

	// Use this for initialization
	void Start () {
        bonus = 200;
        multiplier = 1;
        score = 0;
        flawless = true;
        scoreText = GetComponent<Text>();
        updateScore();
	}

    void updateScore()
    {
        scoreText.text = score.ToString();
    }

    void changeScore(int amount)
    {
        score += amount;
        updateScore();
    }

    void flawlessReset()
    {
        flawless = true;
    }

    void notFlawless()
    {
        flawless = false;
    }

    void OnEnable()
    {
        Tile.OnHit += notFlawless;
        dragonAttackController.attackBegin += flawlessReset;
        dragonAttackController.attackEnd += checkForBonus;
    }

    void OnDisable()
    {
        Tile.OnHit -= notFlawless;
        dragonAttackController.attackBegin -= flawlessReset;
        dragonAttackController.attackEnd += checkForBonus;
    }

    void checkForBonus()
    {
        if(flawless)
        {
            multiplier += 1;
            changeScore(bonus*multiplier);
            Notification.flashMessage("FLAWLESS");
        
        }
        else
        {
            multiplier = 1;
        }
    }


}
