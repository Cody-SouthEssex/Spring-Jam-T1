using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Manager : MonoBehaviour
{
    public List<NavMeshAgent> agents = new List<NavMeshAgent>();
    public GameObject goalLocation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in a)
        {
            agents.Add(go.GetComponent<NavMeshAgent>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (NavMeshAgent a in agents)
        {
            float distance = Vector3.Distance(a.transform.position, goalLocation.transform.position);
            a.isStopped = false;
            a.SetDestination(goalLocation.transform.position);
        }
    }
}
