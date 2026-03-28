using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallGame.Gameplay.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public PanelMatchResult panelMatchResult;

        //Initialize time scale
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            Time.timeScale = 1;
        }

        #region Events

        /// <summary>
        /// Showing panel win
        /// </summary>
        public void Win()
        {
            Time.timeScale = 0;
            panelMatchResult.Show(PanelMatchResult.MatchResult.Win);
        }
        /// <summary>
        /// Showing panel lose
        /// </summary>
        public void Lose()
        {
            Time.timeScale = 0;
            panelMatchResult.Show(PanelMatchResult.MatchResult.Lose);
        }
        //Reloading scene
        public void OnBtnRestartClick()
        {
            DOTween.KillAll();
            Time.timeScale = 1;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        #endregion
    }

}