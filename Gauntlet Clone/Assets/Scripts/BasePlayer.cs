using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/13/2024
*  Script Description: Handles the BasePlayer class that all players inherit from
*/

public class BasePlayer : Subject
{
    public PlayerType playerType;

    public event Action PlayerLowHealth;

    public static event Action<GameObject> PlayerDeath;

    [Header("Projectile Info")]
    [SerializeField] protected GameObject _playerProjectile;
    [SerializeField] protected float _projectileSpeed = 10f;
    [SerializeField] protected float _projectileDamage = 1f;
    [SerializeField] protected float _projectileCooldown = 1f;

    [Header("Magic Info")]
    [SerializeField] protected GameObject _magicProjectile;
    [SerializeField] protected float _magicDamage = 1f;
    [SerializeField] protected float _magicCooldown = 1f;

    [Header("Melee Info")]
    [SerializeField] protected float _meleeRange = 2f;
    [SerializeField] protected float _meleeDamage = 1f;
    [SerializeField] protected float _meleeCooldown = 1f;

    [Header("Player Stats")]
    [SerializeField, Min(1)] protected int _startingArmor = 100;
    [SerializeField, Min(1)] protected float _startingHealth = 1000f;
    [SerializeField] protected float _playerHealthTick = 0.25f;


    protected float _maxHealth = 1000f;
    protected bool canProjectile = true, canMagic = true, canMelee = true;

    public PlayerData PlayerData { get; protected set; }

    private bool triggerNarratorHealthDialogue = true;

    protected virtual void Awake()
    {
        _maxHealth = _startingHealth;

        Attach(NarratorManager.Instance);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Enemy Projectiles
        if (other.CompareTag("Rock"))
        {
            TakeDamage(30);
            Destroy(other.gameObject);
            Debug.Log("Hit by rock, Damage: 3");
        }
        else if (other.CompareTag("Fireball"))
        {
            TakeDamage(50);
            Destroy(other.gameObject);
            Debug.Log("Hit by fireball, Damage: 5");
        }
        else if (other.CompareTag("SorcererSpell"))
        {
            TakeDamage(70);
            Destroy(other.gameObject);
            Debug.Log("Hit by SorcererSpell, Damage: 7");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Door"))
        {
            if (this.PlayerData.Keys > 0)
            {
                this.PlayerData.Keys--;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.transform.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject);
            NarratorManager.Instance.AddTextToQueue(playerType.ToString() + " has been betrayed!");
        }
    }

    private IEnumerator ProjectileCountdown(GameObject go)
    {
        yield return new WaitForSeconds(2);

        if (go)
            Destroy(go);
    }

    private IEnumerator ProjectileCooldown() 
    {
        canProjectile = false;
        yield return new WaitForSeconds(_projectileCooldown);
        canProjectile = true;
    }

    private IEnumerator MagicCooldown()
    {
        canMagic = false;
        yield return new WaitForSeconds(_magicCooldown);
        canMagic = true;
    }

    private IEnumerator MeleeCooldown()
    {
        canMelee = false;
        yield return new WaitForSeconds(_meleeCooldown);
        canMelee = true;
    }

    protected virtual IEnumerator HealthDecrement()
    {
        yield return new WaitForSeconds(_playerHealthTick);
        TakeDamage(1, true);

        StartCoroutine(HealthDecrement());
    }

    private void OnEnable()
    {
        StartCoroutine(HealthDecrement());
    }

    public virtual void Attack1() //Ranged
    {
        if (!canProjectile) return;
        Debug.Log(_playerProjectile);
        GameObject proj = Instantiate(_playerProjectile, transform.position, Quaternion.identity);

        StartCoroutine(ProjectileCountdown(proj));

        proj.GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
        ProjectileInfo info = proj.AddComponent<ProjectileInfo>();
        info.damage = PlayerData.Damage;

        StartCoroutine(ProjectileCooldown());
    }

    public virtual void Attack2() //Magic
    {
        if (!canMagic) return;
        if (_magicProjectile == null) return;

        GameObject proj = Instantiate(_magicProjectile, transform.position, Quaternion.identity);

        StartCoroutine(ProjectileCountdown(proj));

        proj.GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
        ProjectileInfo info = proj.AddComponent<ProjectileInfo>();
        info.damage = PlayerData.MagicDamage;

        StartCoroutine(MagicCooldown());
    }

    public virtual void Attack3() //Melee
    {
        if (!canMelee) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position + (transform.forward * _meleeRange), _meleeRange);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<BaseEnemy>().Damage(PlayerData.MeleeDamage);
            }
        }
        StartCoroutine(MeleeCooldown());
    }

    public virtual void TakeDamage(float amount, bool bypassArmor = false)
    {
        if (amount < 0 && PlayerData.Health >= _maxHealth) return;

        if (!bypassArmor && PlayerData.Armor > 0)
        {
            PlayerData.Armor -= amount;

            if (PlayerData.Armor < 0)
                PlayerData.Armor = 0;
            return;
        }


        PlayerData.Health -= amount;


        if (PlayerData.Health < 200 && triggerNarratorHealthDialogue)
        {
            triggerNarratorHealthDialogue = false;
            Notify();
        }

        if (PlayerData.Health >= 200)
            triggerNarratorHealthDialogue = true;

        if (PlayerData.Health <= 0)
        {
            PlayerDeath?.Invoke(gameObject);
        }
    }

    public virtual void IncreasePoints(int amount)
    {
        PlayerData.CurrentScore += amount;

        if (PlayerData.CurrentScore <= 0)
        {
            PlayerData.CurrentScore = 0;
            NarratorManager.Instance.AddTextToQueue(playerType.ToString() + " is very poor.");
        }
    }

    /// <summary>
    /// It's like a constructor, except it's not ;)
    /// </summary>
    /// <param name="info">The info to "construct" with</param>
    public virtual void ConstructWithInfo(PlayerInfo info)
    {
        _playerProjectile = info.playerProjectile;
        _projectileSpeed = info.projectileSpeed;
        _projectileDamage = info.projectileDamage;
        _magicProjectile = info.magicProjectile;
        _magicDamage = info.magicDamage;
        _meleeDamage = info.meleeDamage;
        _meleeRange = info.meleeRange;

        _magicCooldown = info.magicCooldown;
        _projectileCooldown = info.projectileCooldown;
        _startingArmor = info.startingArmor;
        _meleeCooldown = info.meleeCooldown;

        PlayerData = new PlayerData(_startingHealth, _projectileDamage, _magicDamage, _meleeDamage, _startingArmor);
    }

    public void Reset()
    {
        PlayerData = new PlayerData(_startingHealth, _projectileDamage, _magicDamage, _meleeDamage, 100);
        StopAllCoroutines();
    }
}