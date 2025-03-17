using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CQB_Enemy_Script : MonoBehaviour
{
    public int playerHealth = 5;
    public float attackCooldown = 2.0f;
    public bool hasAttacked = false;

    public GameObject goal;
    Vector3 direction;
    public float rotateSpeed = 3.0f;
    public float moveSpeed = 2.0f;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*direction = goal.transform.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        //transform.Translate(0, 0, moveSpeed * Time.deltaTime);*/
        if (hasAttacked ==true)
        {            
            attackCooldown -= Time.deltaTime;
        }
        if (attackCooldown <= 0)
        {
            attackCooldown = 2.0f;
            hasAttacked = false;
        }
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.gameObject.tag == "Player" && hasAttacked == false)
        {
            playerHealth--;
            hasAttacked = true;            
        }
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, goal.transform.position);
        agent.isStopped = false;
        agent.SetDestination(goal.transform.position);
    }
}
    

