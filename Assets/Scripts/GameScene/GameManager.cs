using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Game states
    public enum GameState
    {
        /// <summary>
        /// Game is not finished
        /// </summary>
        Playing,
        /// <summary>
        /// Player Lost
        /// </summary>
        GameOver,
        /// <summary>
        /// Player Won
        /// </summary>
        Victory
    }

    GameState _currentGameState;

    //parent Transform of the bricks
    public Transform bricksHolder;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    private void Start()
    {
        //Get static singleton as this GameManager
        instance = GetComponent<GameManager>();
        //Set current GameState as Playing 
        _currentGameState = GameState.Playing;
    }

    /// <summary>
    /// Change GameState to specific one
    /// </summary>
    /// <param name="newGameState">Potential next GameState</param>
    void ChangeGameState(GameState newGameState)
    {
        //NOTE: changing to GameOver or Victory states is possible only from Playing state
        
        //if next state is Victory
        if (newGameState == GameState.Victory && _currentGameState == GameState.Playing)
        {
            //activate victory screen
            victoryPanel.SetActive(true);
        }
        //if next state is GameOver
        else if (newGameState == GameState.GameOver && _currentGameState == GameState.Playing)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            //otherwise, do nothing and return
            return;
        }
        //change current GameState to new one
        _currentGameState = newGameState;
    }

    /// <summary>
    /// Check Victory conditions
    /// </summary>
    public void CheckWinCondition()
    {
        //if there are no bricks left (ALERT: ==1 due to bricksHolder counts itself as first child) 
        if(bricksHolder.childCount == 1)
        {
            //Win the game and change GameState
            ChangeGameState(GameState.Victory);
        }
    }

    /// <summary>
    /// Lose the game
    /// </summary>
    public void GameOver()
    {
        //Change GameState to GameOver
        ChangeGameState(GameState.GameOver);
    }
}
