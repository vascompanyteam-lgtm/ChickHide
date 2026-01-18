using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] objectsToSpawn; // Массив префабов для спавна объектов
    public float spawnRadius = 0f;      // Радиус спавна объектов
    public Transform storageObject;      // Родитель для спавненных объектов

    [Header("Interval Settings")]
    public float startSpawnInterval = 1f;  // Начальный интервал между спаунами
    public float minSpawnInterval = 0.1f;  // Минимальный интервал
    public float intervalDecreaseRate = 0.01f; // На сколько уменьшается интервал каждый спавн

    private float spawnInterval;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void OnEnable()
    {
        // При включении спавнера устанавливаем стартовый интервал
        spawnInterval = startSpawnInterval;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled) // Пока объект активен
        {
            SpawnRandomObject();

            // Плавное уменьшение интервала
            spawnInterval -= intervalDecreaseRate;
            spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomObject()
    {
        if (objectsToSpawn.Length == 0) return;

   

        // Выбираем случайный префаб из массива
        GameObject prefabToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Спавним объект в выбранной позиции и добавляем его в список
        GameObject spawnedObject = Instantiate(prefabToSpawn,transform.position, Quaternion.identity, storageObject);
        spawnedObjects.Add(spawnedObject);
    }

    public void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
