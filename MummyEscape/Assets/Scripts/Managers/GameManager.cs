using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    StartGamePlay,
    Won,
    Lost,
    GameCompleted,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState { get; private set; }

    private const string LEVEL = "Level";

    private int levelNo;

    private void Awake()
    {
        Instance = this;
        levelNo = PlayerPrefs.GetInt(LEVEL, 0);
    }

    private void Start()
    {
        ChangeState(GameState.StartGamePlay);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;

        switch (newState)
        {
            case GameState.StartGamePlay:
                Initialize();
                break;
            case GameState.Won:
                LevelWon();
                break;
            case GameState.Lost:
                LevelLost();
                break;
            case GameState.GameCompleted:
                GameCompleted();
                break;
            default:
                break;
        }
    }

    #region GameState Functions

    private void Initialize()
    {
        UIManager.Instance.Init();
    }

    private void LevelWon()
    {
        UIManager.Instance.ShowLevelWonPanel();
    }

    private void LevelLost()
    {
        UIManager.Instance.ShowLevelLostPanel();
    }

    private void GameCompleted()
    {
        UIManager.Instance.ShowGameCompletedPanel();
    }

    #endregion

    public void ResetLevels()
    {
        levelNo = 0;
        PlayerPrefs.SetInt(LEVEL, levelNo);
    }

    public void ReloadScene(int index)
    {
        ChangeState(GameState.StartGamePlay);
        AudioManager.Instance.PlaySound(AudioManager.Instance.buttonClickSfx);
        SceneManager.LoadScene(index);
    }
}
