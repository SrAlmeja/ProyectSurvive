using UnityEngine;
using UnityEngine.UI;

public class WinLoseConditionalController : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    public void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    public void ShowDefeat()
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
    }
}