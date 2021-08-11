
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    [HideInInspector] public int Difficulty;
    [HideInInspector] public bool gameOver = false;
    private bool _pause = false;
    private int _score, _bestScore;

    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt("best");
        Difficulty = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("best", _bestScore);
        }

        if (_score % 15 == 0)
            Difficulty++;

        UIManager.Instance.UpdateScoreText(_score);
    }
    public void GameOver()
    {
        gameOver = true;
        UIManager.Instance.GameOverActivate(true, _score, _bestScore);
        ErrorManager.Instance.FixErrors();
    }
    public void Pause()
    {
        _pause = !_pause;
        UIManager.Instance.PauseActivate(_pause);
        Time.timeScale = _pause ? 0 : 1;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
