using TMPro;
using UnityEngine;

public class PanelMatchResult : MonoBehaviour
{
    public enum MatchResult
    {
        Win, Lose
    }
    [SerializeField] private TMP_Text textResult;

    public void Show(MatchResult matchResult)
    {
        gameObject.SetActive(true);

        if(matchResult == MatchResult.Win)
        {
            textResult.text = "You Win!";
        }
        else
        {
            textResult.text = " You Lose!";
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
