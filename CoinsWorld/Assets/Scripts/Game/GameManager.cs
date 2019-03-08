using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    menu,
    inTheGame,
    gameOver,
    wonTheGame
}

public class GameManager : MonoBehaviour
{
    // Actual GameState
    [HideInInspector]
    public GameState currentGameState;

    // singleton
    public static GameManager sharedInstance;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    // public Canvas gameCanvasVR;
    public Canvas gameOverCanvas;
    public Canvas gameWonCanvas;

    public bool theGameStart = false;

    void Awake()
    {
        // Initialize the singleton and share all the GameManager fields and methods with it
        sharedInstance = this;
    }

    void Start()
    {
        currentGameState = GameState.menu;

        // Setup the canvas behaviour
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        // gameCanvasVR.enabled = false;
        gameOverCanvas.enabled = false;
        gameWonCanvas.enabled = false;
    }

    void Update()
    {
        // If the user want to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTheGame();
        }

        if (currentGameState != GameState.inTheGame && currentGameState != GameState.wonTheGame && GlobalStaticVariables.theUserResetTheGame == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartGame();
            }
        }
        else if (currentGameState != GameState.inTheGame && currentGameState != GameState.wonTheGame && GlobalStaticVariables.theUserResetTheGame == true)
        {
            StartGame();
        }
        else if (currentGameState == GameState.wonTheGame)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                // Reload the scene
                GameSceneManager.sharedInstance.GameScene();
            }
            //else if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    QuitTheGame();
            //}
        }
    }

    // Use this for start the game
    public void StartGame()
    {

        GlobalStaticVariables.theUserResetTheGame = false;
        UICountDown.sharedInstance.StartTheCountDown();
        UICountDown.sharedInstance.PlayerMovement();
        theGameStart = true;
        ChangeGameState(GameState.inTheGame);
    }

    // Called when the player dies
    public void GameOver()
    {
        theGameStart = false;
        ChangeGameState(GameState.gameOver);
    }

    // Called when the player dies
    public void GameWon()
    {
        GlobalStaticVariables.theUserResetTheGame = true;
        theGameStart = false;
        ChangeGameState(GameState.wonTheGame);
    }

    void QuitTheGame()
    {
        Application.Quit();
    }

    // This method will manage the states of the game
    void ChangeGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // The logic of the principal menu
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.inTheGame)
        {
            // This is the current scene or level of the game
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            // gameCanvasVR.enabled = true;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            // Gameover
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = true;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.wonTheGame)
        {
            // Gamewon
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = true;
        }

        // This is the new state after the change 
        currentGameState = newGameState;

    }
}
