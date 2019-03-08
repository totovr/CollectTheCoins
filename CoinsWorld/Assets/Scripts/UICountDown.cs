using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class UICountDown : MonoBehaviour
{
    public static UICountDown sharedInstance;

    public static float TimerBonus = 0;

    public int GameTime = 60;

    private Text _countDownText;

    private GameObject player;
    private FirstPersonController characterControllerScript;
    private AudioSource playerAudio;

    private float _countDownTimerDuration;
    private float _countDownTimerStartTime;

    string timerMessage;
    int timeLeft;

    [HideInInspector]
    public bool theGameIsCounting = true;

    void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _countDownText = GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("PlayerFPS");
        characterControllerScript = player.GetComponent<FirstPersonController>();
        playerAudio = player.GetComponent<AudioSource>();

        characterControllerScript.m_WalkSpeed = 0.0f;
        characterControllerScript.m_RunSpeed = 0.0f;

        // SetUpCountDownTimer(GameTime); // this is the time that will be provide to the user
    }

    // This is a probe
    public void StartTheCountDown()
    {
        SetUpCountDownTimer(GameTime); // this is the time that will be provide to the user
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.theGameStart == true && theGameIsCounting == true)
        {
            if (TimerBonus > 0)
            {
                _countDownTimerStartTime += TimerBonus;
                TimerBonus = 0; // reset the variable or it will generate a bug
            }

            timeLeft = (int)CountDownTimeRemaning();
            if (timeLeft > 0)
            {
                timerMessage = "Timer: " + LeadingZero(timeLeft);
            }
            else
            {
                timerMessage = "Game Over";
                StopPlayerMovement();
                GameManager.sharedInstance.GameOver();
                Debug.Log("The game is finished"); 
            }

            _countDownText.text = timerMessage;
        }
    }

    // This will setup the timing 
    void SetUpCountDownTimer(float DelayInSeconds)
    {
        _countDownTimerDuration = DelayInSeconds;
        _countDownTimerStartTime = Time.time;
    }

    private float CountDownTimeRemaning()
    {
        float _elapsedSeconds = Time.time - _countDownTimerStartTime;
        float _timeLeft = _countDownTimerDuration - _elapsedSeconds;
        return _timeLeft;
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(3, '0');
    }

    public void PlayerMovement()
    {
        characterControllerScript.m_WalkSpeed = 5.0f;
        characterControllerScript.m_RunSpeed = 10.0f;
    }

    public void StopPlayerMovement()
    {
        characterControllerScript.m_WalkSpeed = 0.0f;
        characterControllerScript.m_RunSpeed = 0.0f;
    }

}
