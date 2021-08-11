using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    [Header("Error PopUp")]
    [SerializeField] private GameObject ErrorPopUpPanel;

    [Header("Error Fixed")]
    [SerializeField] private GameObject ErrorFixedPanel;

    [Header("Find Error Panel")]
    [SerializeField] private GameObject ErrorFindPanel;

    [Header("Error Info Panel")]
    [SerializeField] private GameObject ErrorInfoPanel;
    [SerializeField] private Text errorTitleText, errorDescriptionText;
    [SerializeField] private Image errorImage;
    [SerializeField] private Image errorFixBar;


    [Header("Min Map")]
    [SerializeField] private GameObject miniMap;


    [Header("Bullet")]
    [SerializeField] private Text bulletText;


    [Header("Score")]
    [SerializeField] private Text scoreText;


    [Header("Game UI")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text gameOverBestScoreText;

    [SerializeField] private GameObject PausePanel;

    public void ActivateErrorPopUp()
    {
        ErrorPopUpPanel.SetActive(true);
    }
    public void ActivateErrorFixedPanel(bool value)
    {
        ErrorInfoPanel.SetActive(false);
        ErrorFixedPanel.SetActive(value);
    }
    public void ActivateErrorFindPanel()
    {
        ErrorPopUpPanel.SetActive(false);
        ErrorFindPanel.SetActive(true);
    }
    public void ActivateErrorInfoPanel(Error error)
    {
        errorFixBar.fillAmount = 0;
        errorTitleText.text = error.Title;
        errorDescriptionText.text = error.Description;
        errorImage.sprite = error.ErrorSprite;

        ErrorFindPanel.SetActive(false);
        ErrorInfoPanel.SetActive(true);
    }
    public void UpdateErrorFixBar(float value) => errorFixBar.fillAmount = value;

    public void CloseAllPanels()
    {
        ErrorPopUpPanel.SetActive(false);
        ErrorInfoPanel.SetActive(false);
        ErrorFindPanel.SetActive(false);
        ErrorFixedPanel.SetActive(false);
    }

    public void UpdateBulletText(int value) => bulletText.text = value < 0 ? "???" : value.ToString();
    public void UpdateScoreText(int value) => scoreText.text = value.ToString();


    public void ActivateMiniMap(bool activate) => miniMap.SetActive(activate);

    public void GameOverActivate(bool activate,int score,int bestScore)
    {
        CloseAllPanels();
        gameOverScoreText.text= "Skor: " + score;
        gameOverBestScoreText.text= "En İyi Skor: " + bestScore;
        GameOverPanel.SetActive(activate);
    }
    public void PauseActivate(bool activate) => PausePanel.SetActive(activate);

}
