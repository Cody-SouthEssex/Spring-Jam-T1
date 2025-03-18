using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private InputHandler ih;
    public J_Timer magicCooldown;
    public GameObject projectile;
    public Transform firePoint;
    public ProjectileSpawning projectileSpawning;

    // Start is called before the first frame update
    void Start()
    {
        projectileSpawning = GetComponent<ProjectileSpawning>();
        ih = FindObjectOfType<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ih.GetInput(Control.Primary, KeyPressType.Held))
        {
            if (magicCooldown.IsComplete())
            {
                Fire();
                magicCooldown.Start();
            }
        }
    }

    private void Fire()
    {
        projectileSpawning.SpawnProjectiles();
    }
}
