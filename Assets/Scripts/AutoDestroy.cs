using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Settings")]
    public float lifetime = 3f;      // Время жизни объекта
    public bool decreaseLife = true; // Нужно ли забирать жизнь при уничтожении

    private void OnEnable()
    {
        // Запускаем корутину автоудаления
        StartCoroutine(DestroyAfterTime());
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        // Если нужно забирать жизнь
        if (decreaseLife)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.DecreaseLives(1);
            }
        }

        // Уничтожаем объект
        Destroy(gameObject);
    }
}
