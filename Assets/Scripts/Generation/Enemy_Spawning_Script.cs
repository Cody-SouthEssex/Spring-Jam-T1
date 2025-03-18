// Sid

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawning_Script : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform spawnPointsParent;
    [SerializeField] Transform enemyParent;



    // Start is called before the first frame update
    void Awake()
    {      
        List<GameObject> spawnLocations = new List<GameObject>();
        foreach (Transform spawnPoint in spawnPointsParent)
        {
            spawnLocations.Add(spawnPoint.gameObject);
        }
        foreach (GameObject spawnLocation in spawnLocations)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLocation.transform.position, Quaternion.identity, enemyParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
