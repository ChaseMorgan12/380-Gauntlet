using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/03/2024
*  Script Description: Handles behavior for the Theif Enemy
*/

public class Thief : BaseEnemy
{
    //Initializing unique enemy variables////////
    private GameObject richestPLayer;
    
    private float playerMeleeRange = 2f;

    private int _richestPlayerScore = 0;
    private int richestPlayerIndex = 0;

    private void Awake()
    {
        damage = 10;
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
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private void MoveTowardsRichestPlayer()
    {
        richestPLayer = FindRichestPlayer();
        transform.LookAt(richestPLayer.transform);
        transform.position = Vector3.MoveTowards(transform.position, richestPLayer.transform.position, moveRange);
    }
    private GameObject FindRichestPlayer() //will move to closest player by default
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //TEMPORYARY FUNCTION REDO AFTER MERGE UPDATE///////////////////
            if (hitColliders[i].CompareTag("Player"))
            {
                return hitColliders[i].gameObject;
            }

            /*if (hitColliders[i].CompareTag("Player") && hitColliders[i].GetComponent<BasePlayer>().PlayerData.currentScore < _richestPlayerScore)
            {
                continue;
            }
            else if (hitColliders[i].CompareTag("Player") && hitColliders[i].GetComponent<BasePlayer>().PlayerData.currentScore >= _richestPlayerScore)
            {
                //_richestPlayerScore = hitColliders[i].GetComponent<BasePlayer>().PlayerData.currentScore;
                richestPlayerIndex = i;
            }*/
        }
        return hitColliders[richestPlayerIndex].gameObject;
    }
    private void Steal()
    {
        //Attack for 10 damage


        //Steal upgrade potions first
        //If no upgrade potions, steal potions, keys, score, or multiplayer bonus multiplier
        
    }
}
