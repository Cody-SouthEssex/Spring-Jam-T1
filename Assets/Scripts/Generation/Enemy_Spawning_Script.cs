// Sid

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawning_Script : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform spawnPointsParent;
    [SerializeField] Transform enemyParent;
    private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] float spawnDelay;
    [SerializeField] GameObject spawnWarning;


    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = new List<GameObject>();
        foreach (Transform spawnPoint in spawnPointsParent)
        {
            spawnPoints.Add(spawnPoint.gameObject);
        }

        foreach (GameObject spawnLocation in spawnPoints)
        {
            GameObject newWarning = Instantiate(spawnWarning, spawnLocation.transform.position, Quaternion.identity, enemyParent);
            if(newWarning.TryGetComponent(out Debris debris))
            {
                debris.lifeTime.duration = spawnDelay;
            }

        }
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        foreach (GameObject spawnLocation in spawnPoints)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLocation.transform.position, Quaternion.identity, enemyParent);
        }
    }
}
