using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField]
    private Ant antPrefab = null;

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
            Ant ant = Instantiate(antPrefab, transform.position, Quaternion.identity);
            ant.transform.SetParent(transform);

            timeToNextSpawn = Time.time + Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }
}
