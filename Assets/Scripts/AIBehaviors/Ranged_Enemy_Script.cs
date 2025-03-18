using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ProjectileSpawning))]
[RequireComponent(typeof(NavMeshAgent))]
public class Ranged_Enemy_Script : MonoBehaviour
{
    private ProjectileSpawning projectileSpawning;
    public float attackCooldown = 3.0f;
    public bool hasAttacked = false;
    NavMeshAgent agent;

    public GameObject goal;
    public float rotateSpeed = 3.0f;
    public float moveSpeed = 2.0f;

    public float stoppingDistance = 6.0f;
    public float fireDistance = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        projectileSpawning = GetComponent<ProjectileSpawning>();
        goal = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, goal.transform.position);
        if (distance > stoppingDistance && distance < fireDistance)
        {
            if (hasAttacked == false)
            {
                projectileSpawning.SpawnProjectiles();
                hasAttacked = true;
            }
        }
         if (hasAttacked == true)
         {
             attackCooldown -= Time.deltaTime;
         }
        if (attackCooldown <= 0)
        {
            attackCooldown = 3.0f;
            hasAttacked = false;
        }

        agent.isStopped = false;
        if (distance <= stoppingDistance)
        {
            agent.SetDestination(transform.position + (transform.position - goal.transform.position).normalized * stoppingDistance);
            //agent.SetDestination(goal.transform.position - (transform.position - goal.transform.position).normalized * stoppingDistance);            
        }
        else
        {
            agent.SetDestination(goal.transform.position);
        }
        
    }   
}
