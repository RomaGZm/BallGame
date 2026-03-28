using TMPro;
using UnityEngine;

namespace BallGame.Gameplay.UI
{
    public class PanelMatchResult : MonoBehaviour
    {
        public enum MatchResult
        {
            Win, Lose
        }
        [SerializeField] private TMP_Text textResult;

        /// <summary>
        /// Showing match result(Win, Lose)
        /// </summary>
        /// <param name="matchResult"></param>
        public void Show(MatchResult matchResult)
        {
            gameObject.SetActive(true);

            if (matchResult == MatchResult.Win)
            {
                textResult.text = "You Win!";
            }
            else
            {
                textResult.text = " You Lose!";
            }
        }
        //Hide panel
        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}

