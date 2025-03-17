using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float radius;

    public float damage;
    public float hitRadius;

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastStep();
    }

    /// <summary>
    /// The phase of projectile movement
    /// </summary>
    protected virtual void RaycastStep()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, speed * Time.deltaTime))
        {
            transform.position = hit.point;
            OnCollision();
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Events that trigger when the projectile collides with another collider
    /// </summary>
    public virtual void OnCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (Collider c in hits)
        {
            if (c.TryGetComponent(out Health health))
            {
                health.ChangeHealth(-damage, transform.forward * speed);
            }
        }

        Destroy(gameObject);
    }
}
