using UnityEngine;
using System.Collections.Generic;

public class PickupSpawner : MonoBehaviour
{
    public float radius = 5f;
    public GameObject[] pickupPrefabs;
    public int maxItems = 3;

    public float minSpawnTime = 3f; 
    public float maxSpawnTime = 6f;

    private float timer;
    private List<GameObject> spawnedItems = new List<GameObject>();

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        spawnedItems.RemoveAll(item => item == null);

        if (spawnedItems.Count < maxItems)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                SpawnRandomPickup();
                ResetTimer();
            }
        }
    }

    void SpawnRandomPickup()
    {
        if (pickupPrefabs.Length == 0) return;

        GameObject selectedPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];

        Vector2 circlePos = Random.insideUnitCircle * radius;
        Vector3 spawnPos = new Vector3(circlePos.x, 0.5f, circlePos.y) + transform.position;

        GameObject item = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        spawnedItems.Add(item);
    }

    void ResetTimer()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
