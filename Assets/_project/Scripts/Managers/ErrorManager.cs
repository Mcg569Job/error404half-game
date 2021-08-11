using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorType { None, Direction, SightDistance, DefectiveWeapons,Motor }

[System.Serializable]
public class Error
{
    public ErrorType errorType;
    public string Title;
    public string Description;
    public Sprite ErrorSprite;

}

public class ErrorManager : MonoBehaviour
{

    public static ErrorManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    [SerializeField] private Error[] _errors;
    public ErrorType ErrorType;
    private float _errorTimeRange = 20;
    private Coroutine coroutine;
    private int _errorId = 0;

    private void Start()
    {
        ErrorType = ErrorType.None;
        Invoke("NewError", _errorTimeRange);
    }

    public void NewError()
    {
        if (GameManager.Instance.gameOver) return;

        Error e = null;

        if (_errorId > _errors.Length - 1)
            e = _errors[Random.Range(0, _errors.Length)];
        else
        {
            e = _errors[_errorId];
            _errorId++;
        }

        ErrorType = e.errorType;

        if (ErrorType == ErrorType.SightDistance)
        {
            RenderSettings.fog = true;
            UIManager.Instance.ActivateMiniMap(false);
        }

        coroutine = StartCoroutine(FixError(e));
    }

    private IEnumerator FixError(Error e)
    {
        UIManager.Instance.ActivateErrorPopUp();
        AudioManager.Instance.PlaySound(AudioType.Error);
        yield return new WaitForSeconds(3);

        UIManager.Instance.ActivateErrorFindPanel();
        AudioManager.Instance.PlaySound(AudioType.Finding, true);
        yield return new WaitForSeconds(5);

        UIManager.Instance.ActivateErrorInfoPanel(e);
        AudioManager.Instance.PlaySound(AudioType.Fixing, true);

        float t = 0;
        float maxTime = Random.Range(5, 15);
        while (t < maxTime)
        {
            t += Time.deltaTime;
            UIManager.Instance.UpdateErrorFixBar(t / maxTime);
            yield return null;
        }

        UIManager.Instance.ActivateErrorFixedPanel(true);
        AudioManager.Instance.PlaySound(AudioType.Fixed);

        ErrorFixed();

        yield return new WaitForSeconds(3);
        UIManager.Instance.ActivateErrorFixedPanel(false);
    }

    private void ErrorFixed()
    {
        if (ErrorType == ErrorType.SightDistance)
        {
            RenderSettings.fog = false;
            UIManager.Instance.ActivateMiniMap(true);
        }
        ErrorType = ErrorType.None;
        Invoke("NewError", _errorTimeRange);
    }

    public void FixErrors()
    {
        UIManager.Instance.CloseAllPanels();
        ErrorFixed();
        CancelInvoke();
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
    public void CreateNewError()
    {
        Invoke("NewError", _errorTimeRange);
    }
}
