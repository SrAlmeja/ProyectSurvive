using System;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI worldTimerText;
    private float remainingTime;

    private CoreLogic coreLogic;
    private void Awake()
    {
        coreLogic = FindObjectOfType<CoreLogic>();
        if (coreLogic == null)
        {
            Debug.LogError("No se encontró el objeto con el componente CoreLogic en la escena.");
        }
    }

    private void Start()
    {
        remainingTime = coreLogic.TimeToWin;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        
        TimerTextController();
    }

    private void TimerTextController()
    {
        if (timerText != null)
        {
            timerText.text = "Estimated time to repair Space-Magic-Time Machine: " + Mathf.RoundToInt(remainingTime).ToString();
            worldTimerText.text = Mathf.RoundToInt(remainingTime).ToString();
            if (remainingTime <= 0)
            {
                timerText.text = "The SMT machine has been repaired, return to it to evacuate";
                worldTimerText.text = "(See you next time Wizard...)";
            }
        }
    }
}