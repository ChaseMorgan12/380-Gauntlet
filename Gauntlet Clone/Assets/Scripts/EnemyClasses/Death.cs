using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/04/2024
*  Script Description: Handles behavior for the Death Enemy
*  Death, a special enemy that is never generated, cannot be slowed by fighting or shooting it. 
*  However, any type of potion attack will kill it instantly, no matter how weak.
*  
*  Shooting Death only gives a single point for every hit.
*  However, what is very lucrative for scoring is using a potion on Death. By default, you score 1000 points. 
*  There is actually a variable points value for killing Death with a potion. 
*  Every time you hit Death with a shot, you cycle the value. By default, it is 1000 points. 
*  The full cycle is, starting from default: 1000, 2000, 1000, 4000, 2000, 6000, 8000, and then back to the default 1000. 
*  If you want to get the most points, shoot death exactly 6 times.
*/

public class Death : BaseEnemy
{
    private float sapRange = 1f;
    private bool canSap = true;

    //Index of how many times death has been hit by a player
    private int hitIndex = 1;
    
    private int pointValue = 1000;

    private void Awake()
    {
        //Initializing unique enemy variables////////
        speed = .5f; //Moves slower than other enemies????
        detectionRange = 25f;
        moveRange = 0.1f;
        damage = 75f; //damage should be low since it can sap for every half second
        _health = 10000000000000000; 
    }

    private void FixedUpdate()
    {
        if (PlayerInSapRange())
        {
            if (canSap)
            {
                StartCoroutine(SapTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void SapHealth() //it saps HP rapidly until either contact is broken, or it reaches the limit of 200 health and disappears.
    {
        Debug.Log("Sapping Player Health");
        _player.GetComponent<BasePlayer>().TakeDamage(damage);
    }
    private bool PlayerInSapRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sapRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can sap player health if player is found in range
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(_player.transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator SapTimer()
    {
        canSap = false;
        SapHealth();
        yield return new WaitForSeconds(.5f);//Saps health fast, every half second
        canSap = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            GotHit();
        }
    }
    private void GotHit()//The full cycle is, starting from default: 1000, 2000, 1000, 4000, 2000, 6000, 8000, and then back to the default 1000. 
    {
        if (hitIndex == 7)
        {
            hitIndex = 1;
        }
        else
        {
            hitIndex++;
        }
        switch (hitIndex)
        {
            case 1:
                pointValue = 1000;
                break;
            case 2:
                pointValue = 2000;
                break;
            case 3:
                pointValue = 1000;
                break;
            case 4:
                pointValue = 4000;
                break;
            case 5:
                pointValue = 2000;
                break;
            case 6:
                pointValue = 6000;
                break;
            case 7:
                pointValue = 8000;
                break;
            default:
                Debug.LogError("Error: Wrong Death hitIndex: " + hitIndex);
                break;
        }
    }
    private void OnDestroy() 
    {
        Pickup.lastPotionPlayer.GetComponent<BasePlayer>().IncreasePoints(pointValue);
        //Gives points based on how much it has been hit
        _player.GetComponent<BasePlayer>().IncreasePoints(pointValue);
    }
}
