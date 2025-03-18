using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mixed_Range_Enemy : MonoBehaviour
{
    private ProjectileSpawning projectileSpawning;
    private EntityData enemyData;
    public float attackCooldown = 2.0f;
    public bool hasAttacked = false;
    public float damage = 5;

    public GameObject goal;
    Vector3 direction;
    public float rotateSpeed = 3.0f;
    public float moveSpeed = 2.0f;

    NavMeshAgent agent;
    [SerializeField] float stoppingDistance = 4.0f;
    [SerializeField] float fireDistance = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = GetComponent<EntityData>();
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
        if (hasAttacked == true)
        {
            attackCooldown -= Time.deltaTime;
        }
        if (attackCooldown <= 0)
        {
            attackCooldown = 3.0f;
            hasAttacked = false;
        }

        float distance = Vector3.Distance(transform.position, goal.transform.position);
        if (distance > fireDistance)
        {
            agent.SetDestination(goal.transform.position + (transform.position - goal.transform.position).normalized * fireDistance);
            if (hasAttacked == false)
            {
                projectileSpawning.SpawnProjectiles();
                hasAttacked = true;
            }
        }
        
        if (distance > stoppingDistance && distance < fireDistance)
        {
            agent.SetDestination(transform.position + (transform.position - goal.transform.position).normalized * stoppingDistance);            
        }       

        agent.isStopped = false;
        if (distance <= stoppingDistance)
        {
            agent.SetDestination(goal.transform.position);
            if (distance < 1.5f)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, 1);

                foreach (Collider hit in hits)
                {
                    if (hit.TryGetComponent(out EntityData entityData))
                    {
                        if (entityData.team != enemyData.team || entityData.team == Team.None)
                        {
                            if (hit.TryGetComponent(out Health health))
                            {
                                health.ChangeHealth(-damage);
                            }
                        }
                    }
                }
                hasAttacked = true;
            }
        }    
       

    }
}
