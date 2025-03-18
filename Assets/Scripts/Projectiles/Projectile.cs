using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(EntityData))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public EntityData entityData;

    public GameObject onHitEffect;

    public float speed;
    public float radius;

    public float damage;
    public float hitRadius;

    public float rotationBase = 0;
    public float rotationAccel = 0;

    public float lifeTime;

    public float knockbackDampen = 2;

    // Start is called before the first frame update
    void Start()
    {
        entityData = GetComponent<EntityData>();

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
        rotationBase /= rotationAccel;
        if (rotationBase > 0)
        {
            transform.rotation *= Quaternion.Euler(0, rotationBase * Time.deltaTime, 0);
        }

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
            if (c.TryGetComponent(out EntityData hitEntityData))
            {
                if (hitEntityData.team != Team.None)
                {
                    if (hitEntityData.team == entityData.team)
                    {
                        continue;
                    }
                }
            }
            if (c.TryGetComponent(out Health health))
            {
                health.ChangeHealth(-damage, transform.forward * speed / knockbackDampen);
            }
            if (c.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(transform.forward * speed / knockbackDampen, ForceMode.Impulse);
            }
        }

        if (onHitEffect)
        {
            Instantiate(onHitEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
