using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawning : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    private EntityData entityData;

    [Header("Variables")]
    public int burstCount;
    public float burstDelay;
    public int arcCount;
    public float arcSpread;
    public float arcOffsetBase;
    private float arcOffset;
    public float arcOffsetChange;


    private void Start()
    {
        entityData = GetComponent<EntityData>();
    }

    public void SpawnProjectiles()
    {
        StartCoroutine(Burst());
    }

    private IEnumerator Burst()
    {
        arcOffset = arcOffsetBase;
        for (int i = 0; i < burstCount; i++)
        {
            for(int j = 0; j < arcCount; j++)
            {
                SpawnProjectile(new Vector3(0, arcOffset + (j - arcCount / 2f) * arcSpread, 0));
            }
            yield return new WaitForSeconds(burstDelay);
            arcOffset += arcOffsetChange;
        }
    }

    private void SpawnProjectile(Vector3 angleOffset)
    {
        GameObject newProjectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation * Quaternion.Euler(angleOffset));
        newProjectile.SetActive(true);
        if (newProjectile.TryGetComponent(out EntityData projEntityData))
        {
            projEntityData.team = entityData.team;
        }
    }
}
