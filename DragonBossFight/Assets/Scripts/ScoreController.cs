using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public NotificationController Notification;
    public int defenseBonus;
    public int attackBonus;

    private bool flawless;
    private Text scoreText;
    private int m_Score;
    public int Score
    {
        get { return m_Score; }
        set
        {
            if (m_Score == value) return;
            m_Score = value;
            if (OnVariableChange != null)
                OnVariableChange(m_Score);
        }
    }

    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;


    private int animatedScore;
   
    private int multiplier;

	// Use this for initialization
	void Start () {
        multiplier = 1;
        m_Score = 0;
        flawless = true;
        scoreText = GetComponent<Text>();
        updateScore();
	}

    public IEnumerator updateScore()
    {
        while(animatedScore < m_Score)
        {
            if(animatedScore + 100 < m_Score)
            {
                animatedScore += 100;
            }
            else
            {
                animatedScore += 10;
            }
            scoreText.text = animatedScore.ToString();
            yield return new WaitForEndOfFrame();
        }
  
    }

    void changeScore(int amount)
    {
        Score += amount;
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
        dragonHealth.takeHit += awardAttackBonus;
        OnVariableChange += VariableChangeHandler;
    }

    void OnDisable()
    {
        Tile.OnHit -= notFlawless;
        dragonAttackController.attackBegin -= flawlessReset;
        dragonAttackController.attackEnd += checkForBonus;
        OnVariableChange -= VariableChangeHandler;
    }

    void checkForBonus()
    {
        if(flawless)
        {
            string message = buildMessage();
            Notification.flashMessage(message);
            changeScore(defenseBonus * multiplier);
            multiplier += 1;
        }
        else
        {
            multiplier = 1;
        }
    }

    string buildMessage()
    {
        string message = "FLAWLESS";
        if(multiplier > 1)
        {
            message += " X " + multiplier;
        }
        return message;
    }

    void awardAttackBonus()
    {
        changeScore(attackBonus);
    }


    void VariableChangeHandler(int newVal)
    {
        StartCoroutine(updateScore());
    }


}
