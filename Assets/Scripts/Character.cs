using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // GAME OBJECTS
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Material thisMaterial;

    // STATS
    private float _maxHealth = 100;
    private float _currHealth = 100;
    private int spawnOffset;
    private Vector3 spawnLocation;
    public float GetMaxHealth()
    {
        return this._maxHealth;
    }
    public float GetCurrentHealth()
    {
        return this._currHealth;
    }

    // TRACKERS
    public bool isProjectileTraveling = false;

    // LIFE CYCLE METHODS
    private void Awake()
    {
        // determin projectile spawn location
        spawnOffset  = gameObject.name == "Player" ? 1 : -1;
        spawnLocation = gameObject.transform.position + new Vector3(spawnOffset, 0, 0);
    }

    /// <summary>
    /// Subtract damage amount from this gameObject's health
    /// </summary>
    /// <param name="damage">Amount of damage being done.</param>
    /// <returns>True if this character is still alive, False otherwise.</returns>
    public bool TakeDamage(float damage)
    {
        _currHealth -= (int) damage;
        isProjectileTraveling = false;
        return _currHealth > 0;
    }

    public void ShootProjectile(float powerLevel)
    {
        Projectile newProj = Instantiate(_projectile, spawnLocation, _projectile.transform.rotation);
        newProj.SetDirection(spawnOffset);
        newProj.SetDamage(powerLevel);
        isProjectileTraveling = true;
    }
}
