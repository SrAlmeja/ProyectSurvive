using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI worldTimerText;
    private float remainingTime;
    
    [SerializeField] private TextMeshProUGUI scoreText;

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
        ScoreTextController(); // Llama al método para actualizar el texto del puntaje
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

    private void ScoreTextController()
    {
        StartCoroutine(AnimateScoreText()); // Inicia la animación del texto del puntaje
    }

    private IEnumerator AnimateScoreText()
    {
        int currentScore = 0;
        int targetScore = ScoreManager.Score;
        float duration = 1.0f; // Duración de la animación en segundos
        float timer = 0f;

        while (timer < duration)
        {
            float progress = timer / duration;
            currentScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, progress));
            scoreText.text = "Score: " + currentScore;

            timer += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que el texto muestre el puntaje final exacto
        scoreText.text = "Score: " + targetScore;
    }
}