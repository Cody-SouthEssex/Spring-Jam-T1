using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Corpse : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector] public Vector3 force;
    public float stunDuration;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(StartStun());

        Destroy(gameObject, lifeTime);
    }

    private IEnumerator StartStun()
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(stunDuration);
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
