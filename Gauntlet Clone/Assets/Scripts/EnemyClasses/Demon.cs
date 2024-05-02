using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/02/20024
*  Script Description: Handles behavior for the Demon Enemy
*/

public class Demon : BaseEnemy
{
    public GameObject fireball;

    private float biteRange = 1f;

    private float fireballRange= 10f;
    private float fireballVelocity = 7f;
    private bool canThrowFireball = true;

    private void Awake()
    {
        //Based on ememy level, damage will change
        switch (enemyLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 8;

                break;
            case 3:
                damage = 10;
                break;
            default:
                break;
        }
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInBiteRange())
        {
            Debug.Log("Bitting Player");
        }
        else if (PlayerInFireballRange())
        {
            if (canThrowFireball)
            {
                StartCoroutine(fireballTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void ThrowFireball()
    {
        //instansiate rock prefab towards player
        Debug.Log("Throwing fireball");
        GameObject fireballProj = Instantiate(fireball, transform.position, transform.rotation);
        fireballProj.GetComponent<Rigidbody>().velocity = transform.forward * fireballVelocity;
        StartCoroutine(fireballDestroyTimer(fireballProj));
    }
    private bool PlayerInBiteRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, biteRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can bite player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private bool PlayerInFireballRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, fireballRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can throw fireball towards player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
   
    private IEnumerator fireballTimer()
    {
        canThrowFireball = false;
        ThrowFireball();
        yield return new WaitForSeconds(3.5f);
        canThrowFireball = true;
    }
    private IEnumerator fireballDestroyTimer(GameObject fireballGo)
    {
        yield return new WaitForSeconds(2f);
        Destroy(fireballGo);
    }
}
