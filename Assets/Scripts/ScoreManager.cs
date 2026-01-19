using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textoScore;
    public TMP_Text textoScore2;

    private int scoresBall;

    private const string BEST_SCORE_KEY = "BEST_SCORE";

    void OnEnable()
    {
        scoresBall = 0;
        UpdateText();
    }
    private void OnDisable()
    {
        StopAndSaveBest();
    }

    void UpdateText()
    {
        textoScore.text = "Points:" + scoresBall.ToString();
        textoScore2.text = "Points:" + scoresBall.ToString();
    }

    // ➕ Добавить очки
    public void AddScore(int amount = 1)
    {
        scoresBall += amount;
        UpdateText();
    }

    // 🔴 ВЫЗЫВАТЬ ПРИ КОНЦЕ ИГРЫ
    public void StopAndSaveBest()
    {
        int best = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

        if (scoresBall > best)
        {
            PlayerPrefs.SetInt(BEST_SCORE_KEY, scoresBall);
            PlayerPrefs.Save();
        }
    }

    // (опционально)
    public int GetScore()
    {
        return scoresBall;
    }
}
