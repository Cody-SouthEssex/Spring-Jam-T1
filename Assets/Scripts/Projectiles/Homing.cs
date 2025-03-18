using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Homing : MonoBehaviour
{
    private Projectile projectile;

    public int level = 1;
    public float turnSpeedPerLevel = 5;
    public float turnSpeed;
    public float radiusPerLevel = 1;
    public float radius;

    public Transform target;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        turnSpeed = turnSpeedPerLevel * level;
        radius = radiusPerLevel * level;

        List<Collider> hits = Physics.OverlapSphere(transform.position, radius).ToList();
        hits.Sort((x, y) => GetHeuristic(x.transform.position, transform.position).CompareTo(GetHeuristic(x.transform.position, transform.position)));
        if (hits.Count > 0)
        {
            target = hits[0].transform;
        }

        if (target)
        {
            Quaternion startRotation = transform.rotation;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.RotateTowards(startRotation, transform.rotation, turnSpeedPerLevel * Time.deltaTime);
        }
    }

    private float GetHeuristic(Vector3 a, Vector3 b)
    {
        float xdif = Mathf.Abs(a.x - b.x);
        float zdif = Mathf.Abs(a.z - b.z);
        return xdif + zdif;
    }
}
