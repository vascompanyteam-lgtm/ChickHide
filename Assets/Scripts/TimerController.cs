using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float startTime = 60f; // 1 минута
    public MenuTravel menutravel;
    private float currentTime;
    private bool isRunning;

    private void OnEnable()
    {
        RestartTimer();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateView();

            OnTimerEnd(); // 🔥 ВОТ ОН — КОНЕЦ ТАЙМЕРА
        }
        else
        {
            UpdateView();
        }
    }

    // --------------------
    // TIMER LOGIC
    // --------------------

    public void RestartTimer()
    {
        currentTime = startTime;
        isRunning = true;
        UpdateView();
    }

    // --------------------
    // TIMER END
    // --------------------

    void OnTimerEnd()
    {
        Debug.Log("TIMER END");

        // 👉 ТУТ ТВОЯ ЛОГИКА
        // GameOver();
        // healthController.MinusHealth();
        // gameManager.RestartGame();
        menutravel.makeMenu(2);
       // RestartTimer(); // если нужна бесконечность
    }

    // --------------------
    // VIEW
    // --------------------

    void UpdateView()
    {
        int seconds = Mathf.CeilToInt(currentTime);
        timerText.text = $"{seconds} sec";
    }

}
