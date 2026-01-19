using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Settings")]
    public float lifetime = 3f;      // Время жизни объекта
    public bool makeConfettiEffect = true; // Нужно ли забирать жизнь при уничтожении
    public GameObject effect;
    private void OnEnable()
    {
        // Запускаем корутину автоудаления
        StartCoroutine(DestroyAfterTime());
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        if(makeConfettiEffect)
        {
            Instantiate(effect, transform.position, Quaternion.identity, transform.parent.transform);
        }
        // Уничтожаем объект
        Destroy(gameObject);
    }
}
