using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public J_Timer lifeTime;
    public float shrinkSpeed = 4;
    public bool doShrink;
    ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        lifeTime.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime.IsComplete())
        {
            if (doShrink)
            {
                transform.localScale -= shrinkSpeed * Time.deltaTime * Vector3.one;
                if (transform.localScale.x <= 0.1f)
                {
                    Destroy(gameObject);
                }
                if (transform.localScale.y <= 0.1f)
                {
                    Destroy(gameObject);
                }
                if (transform.localScale.z <= 0.1f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if(particleSystems.Length > 0)
                {
                    foreach (ParticleSystem p in particleSystems)
                    {
                        p.Stop();
                    }
                }
                Destroy(gameObject, 5);
            }
        }
    }
}
