using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public enum Team
{
    Player,
    Enemy,
}

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [Header("Player")]
    public bool isPlayer;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 1;

    public delegate void HealthUpdate(float healthChange);
    public HealthUpdate OnHealthChanged;
    public delegate void DeathEvent();
    public DeathEvent OnDeath;

    public bool isAlive = true;

    [Header("Stun")]
    public bool isStunned;
    public float stunDuration;
    Coroutine stun = null;

    [Header("Corpse")]
    public GameObject corpsePrefab;
    protected Vector3 corpseForce;

    [Header("HitFlash")]
    private MeshRenderer mr;
    private Material defaultMaterial;
    public Material hitFlashMaterial;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defaultMaterial = mr.material;
    }

    /// <summary>
    /// Changes the current health, invokes an event, and checks death status
    /// </summary>
    public void ChangeHealth(float value)
    {
        if (!isStunned)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);

            OnHealthChanged?.Invoke(value);

            CheckDeathStatus();

            if (isAlive)
            {
                if (stun != null)
                {
                    StopCoroutine(stun);
                }
                stun = StartCoroutine(HitStun());
            }
        }
    }
    public void ChangeHealth(float value, Vector3 force)
    {
        if (!isStunned)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);

            OnHealthChanged?.Invoke(value);

            corpseForce = force;

            CheckDeathStatus();

            if (isAlive)
            {
                if (stun != null)
                {
                    StopCoroutine(stun);
                }
                stun = StartCoroutine(HitStun());
            }
        }
    }

    /// <summary>
    /// Triggers events when health is 0 or less
    /// </summary>
    private void CheckDeathStatus()
    {
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            OnDeath?.Invoke();
            SpawnCorpse();

            if (isPlayer)
            {
                // Pause
                Time.timeScale = 0;
            }
        }
    }

    /// <summary>
    /// Place a corpse, and destroy the original
    /// </summary>
    public void SpawnCorpse()
    {
        if (corpsePrefab)
        {
            GameObject newCorpse = Instantiate(corpsePrefab, transform.position, transform.rotation);
            if (newCorpse.TryGetComponent(out Corpse corpse))
            {
                corpse.force = corpseForce;
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator HitStun()
    {
        isStunned = true;
        mr.material = hitFlashMaterial;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        mr.material = defaultMaterial;
    }
}
