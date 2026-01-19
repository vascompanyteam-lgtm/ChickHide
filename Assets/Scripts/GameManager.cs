using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float startDelay = 3f;
    public float winDelay = 2f;

    [Header("Buttons")]
    public Button[] buttons;

    private int hiddenIndex;
    private bool canClick;

    public HealthController healthController;
    public GameObject effectWin;
    public AudioSource audioNice;
    public AudioSource audioBad;
    public ScoreManager scoreManager;
    public SettingsSwitcher settingsSwitcher;
    private void OnEnable()
    {
        StartCoroutine(RestartGame());
    }

    // --------------------
    // RESTART GAME
    // --------------------
    IEnumerator RestartGame()
    {
        canClick = false;

        // сброс визуала
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].image.color = Color.white;

            int index = i; // важно для замыкания
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // стартовый таймер
        yield return new WaitForSeconds(startDelay);

        hiddenIndex = Random.Range(0, buttons.Length);
        canClick = true;

        foreach (var b in buttons)
            b.interactable = true;
    }

    // --------------------
    // CLICK LOGIC
    // --------------------
    void OnButtonClick(int index)
    {
        if (!canClick) return;

        if (index == hiddenIndex)
        {
            // УГАДАЛ
            audioNice.Play();
            canClick = false;
            Instantiate(effectWin, buttons[index].transform.position, Quaternion.identity, buttons[index].transform);
            healthController.AddLife();
            scoreManager.AddScore();
            StartCoroutine(WinRoutine());
        }
        else
        {

            if(settingsSwitcher.dummyOn)
            {
                Handheld.Vibrate();
            }
            audioBad.Play();
            LoseLife();
            buttons[index].image.color = Color.red;
            buttons[index].interactable = false;
        }
    }

    // --------------------
    // WIN
    // --------------------
    IEnumerator WinRoutine()
    {
        WinSuccess();

        yield return new WaitForSeconds(winDelay);

        // полный рестарт
        StartCoroutine(RestartGame());
    }

    // --------------------
    // STUBS (заглушки)
    // --------------------
    void LoseLife()
    {
        healthController.TakeDamage();
    }

    void WinSuccess()
    {
        Debug.Log("WIN (stub)");
    }
}
