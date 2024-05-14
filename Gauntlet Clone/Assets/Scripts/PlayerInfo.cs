using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Info", menuName = "PlayerInfo", order = 1)]
public class PlayerInfo : ScriptableObject
{
    [Header("Projectile Info")]
    public GameObject playerProjectile;
    public float projectileSpeed = 10f;
    public float projectileDamage = 1f;
    public float projectileCooldown = 1f;

    [Header("Magic Info")]
    public GameObject magicProjectile;
    public float magicDamage = 1f;
    public float magicCooldown = 1f;

    [Header("Melee Info")]
    public float meleeRange = 2f;
    public float meleeDamage = 1f;
    public float meleeCooldown = 1f;

    public int startingArmor = 100;
}
