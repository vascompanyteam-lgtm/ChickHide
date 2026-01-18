using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text statusText; // Один TextMeshPro на оба значения
    public Text eggs;
    [Header("Game Settings")]
    public int maxLives = 10;          // Максимум жизней
    private int currentLives;          // Текущие жизни
    private int currentScore;          // Текущие очки

    public MenuTravel menuTravel;      // Для GameOver
    public AudioSource dis;
    private void OnEnable()
    {
        currentLives = maxLives;
        currentScore = 0;
        eggs.text = 0.ToString();
        UpdateStatusUI();
    }

    // Метод для уменьшения жизней
    public void DecreaseLives(int amount = 1)
    {
        currentLives -= amount;
        if (currentLives < 0) currentLives = 0;

        UpdateStatusUI();
        dis.Play();
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    // Метод для добавления очков
    public void AddScore(int amount)
    {
        currentScore += amount;
        eggs.text=currentScore.ToString();
        UpdateStatusUI();
    }

    // Обновление UI (формат: Жизни/Очки)
    private void UpdateStatusUI()
    {
        if (statusText != null)
        {
            statusText.text = $"{currentLives}/{currentScore}";
        }
    }

    private void GameOver()
    {
        if (menuTravel != null)
            menuTravel.makeMenu(5);

     
    }
}
