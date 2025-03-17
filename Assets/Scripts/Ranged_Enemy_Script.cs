using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged_Enemy_Script : MonoBehaviour
{
    public int playerHealth = 5;
    public float attackCooldown = 3.0f;
    public bool hasAttacked = false;
    NavMeshAgent agent;

    public GameObject goal;
    Vector3 direction;
    public float rotateSpeed = 3.0f;
    public float moveSpeed = 2.0f;

    public float stoppingDistance = 6.0f;
    public float fireDistance = 12.0f;

    public GameObject projSource;
    public GameObject fireBall;

    // Start is called before the first frame update
    void Start()
    {
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
                Instantiate(fireBall, projSource.transform.position, Quaternion.identity);
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
