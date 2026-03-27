using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public PanelMatchResult panelMatchResult;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Time.timeScale = 1;
    }

    #region Events

    public void Win()
    {
        Time.timeScale = 0;
        panelMatchResult.Show(PanelMatchResult.MatchResult.Win);
    }
    public void Lose()
    {
        Time.timeScale = 0;
        panelMatchResult.Show(PanelMatchResult.MatchResult.Lose);
    }
    public void OnBtnRestartClick()
    {
        DOTween.KillAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    #endregion
}
