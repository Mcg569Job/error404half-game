
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Text bestScoreText;

    private void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        bestScoreText.text = PlayerPrefs.GetInt("best").ToString("00000");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
