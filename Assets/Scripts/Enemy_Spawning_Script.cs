using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawning_Script : MonoBehaviour
{
    [SerializeField] GameObject rangedEnemy;
    [SerializeField] GameObject CQBEnemy;
    [SerializeField] GameObject spawnPointsParent;
    
    
   
    // Start is called before the first frame update
    void Awake()
    {      

      
        List<GameObject> spawnLocations = new List<GameObject>();
        foreach (Transform spawnPoint in spawnPointsParent.transform)
        {
            spawnLocations.Add(spawnPoint.gameObject);
        }
        foreach (GameObject spawnLocation in spawnLocations)
        {
            int enemyType = Random.Range(0, 2);
            if (enemyType == 0)
            {
                Instantiate(rangedEnemy, spawnLocation.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(CQBEnemy, spawnLocation.transform.position, Quaternion.identity);
            }
        }
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
