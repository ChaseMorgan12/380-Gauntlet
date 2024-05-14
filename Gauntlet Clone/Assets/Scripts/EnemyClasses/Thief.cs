using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/04/2024
*  Script Description: Handles behavior for the Theif Enemy
*/

public class Thief : BaseEnemy
{
    //Initializing unique enemy variables////////
    
    private float playerMeleeRange = 2f;

    private int _richestPlayerScore = 0;
    private int richestPlayerIndex = 0;

    private void Awake()
    {
        damage = 100;
        speed = 10f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInMeleeRange())
        {
            Steal();
        }
        else 
        {
            MoveTowardsRichestPlayer();
        }
    }
    private bool PlayerInMeleeRange() 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerMeleeRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //will steal from player if player is found in range
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(_player.transform);
                return true;
            }
        }
        return false;
    }
    private void MoveTowardsRichestPlayer()
    {
        _player = FindRichestPlayer();
        transform.LookAt(_player.transform);
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, moveRange);
    }
    private GameObject FindRichestPlayer() //will move to closest player by default
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player") && hitColliders[i].GetComponent<BasePlayer>().PlayerData.CurrentScore < _richestPlayerScore)
            {
                continue;
            }
            else if (hitColliders[i].CompareTag("Player") && hitColliders[i].GetComponent<BasePlayer>().PlayerData.CurrentScore >= _richestPlayerScore)
            {
                _richestPlayerScore = (int)hitColliders[i].GetComponent<BasePlayer>().PlayerData.CurrentScore;
                richestPlayerIndex = i;
            }
        }
        return hitColliders[richestPlayerIndex].gameObject;
    }
    private void Steal()
    {
        //Attack for 100 damage
        _player.GetComponent<BasePlayer>().TakeDamage(damage);

        //Steal keys, score, or multiplayer bonus multiplier

    }
}
