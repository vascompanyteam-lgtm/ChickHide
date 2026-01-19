using UnityEngine;

public class HealthController : MonoBehaviour
{
    public MenuTravel menuTravel;

    [Header("Health Settings")]
    public int maxHealth = 3;

    [Header("Health Views")]
    public GameObject[] healthViews;
    // 0 - первая жизнь, 1 - вторая, 2 - третья

    private int currentHealth;

    // --------------------
    // LIFECYCLE
    // --------------------

    private void OnEnable()
    {
        RestartHealth();
    }

    // --------------------
    // HEALTH LOGIC
    // --------------------

    public void TakeDamage(int amount = 1)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateView();

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    // --------------------
    // ADD LIFE
    // --------------------

    public void AddLife(int amount = 1)
    {
        if (currentHealth >= maxHealth)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateView();
    }

    // --------------------
    // VIEW
    // --------------------

    void UpdateView()
    {
        for (int i = 0; i < healthViews.Length; i++)
        {
            if (healthViews[i] != null)
                healthViews[i].SetActive(i < currentHealth);
        }
    }

    // --------------------
    // RESTART
    // --------------------

    public void RestartHealth()
    {
        currentHealth = maxHealth;
        UpdateView();
    }

    // --------------------
    // GAME OVER
    // --------------------

    void GameOver()
    {
        Debug.Log("GAME OVER");
        menuTravel.makeMenu(5);
        // 👉 ТУТ ТВОЯ ЛОГИКА
        // Time.timeScale = 0f;
        // menu.OpenGameOver();
        // player.enabled = false;
    }
}
