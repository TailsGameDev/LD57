using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] antPrefabs = null;

    [SerializeField]
    private float minTimeBetweenSpawns = 0.0f;
    [SerializeField]
    private float maxTimeBetweenSpawns = 0.0f;

    private float timeToNextSpawn;

    private void Awake()
    {
        timeToNextSpawn = Time.time + Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void Update()
    {
        if (timeToNextSpawn < Time.time)
        {
            // Instantiate random ant, and parent it to this game object
            GameObject randomAntPrefab = antPrefabs[Random.Range(0, antPrefabs.Length)];
            GameObject antInstance = Instantiate(randomAntPrefab, transform.position, Quaternion.identity);
            antInstance.transform.SetParent(transform);

            timeToNextSpawn = Time.time + Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            maxTimeBetweenSpawns = maxTimeBetweenSpawns * 0.8f;
            if(maxTimeBetweenSpawns < 5f)
            {
                maxTimeBetweenSpawns = Random.Range(15f, 30f);
            }
        }
    }
}
