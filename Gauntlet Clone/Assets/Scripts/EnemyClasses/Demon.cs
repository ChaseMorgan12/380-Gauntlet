using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public class Demon : BaseEnemy
{
    public GameObject fireball;


    private float biteRange = 1f;
    private float fireballRange = 10f;
    private float fireballVelocity = 5f;

    private bool canBite = true;
    private bool canFireball = true;

    private void Awake()
    {
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInBiteRange())
        {
            if (canBite)
            {
                StartCoroutine(BiteTimer());
            }
        }
        else if (PlayerInFireballRange())
        {
            if (canFireball)
            {
                StartCoroutine(FireballTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void Fireball()
    {
        

    }
    private void Bite()
    {
        //Swing club at player if in club range
        Debug.Log("Biting");

    }
    private void ThrowFireball()
    {
        //instansiate fireball prefab towards player
        //Demon fireballs will damage anything
        //Take care not to accidentally train their shots onto breakable jugs of food or blue potions; their shots destroy such items (and their shots don't activate potions either).
        //Instead, train their shots onto enemies and generators to damage or destroy them.
        Debug.Log("Throwing fireball");
        GameObject fireballProj = Instantiate(fireball, transform.position, transform.rotation);
        fireballProj.GetComponent<Rigidbody>().velocity = transform.forward * fireballVelocity;
        StartCoroutine(FireballDestroyTimer(fireballProj));
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
            if (hitColliders[i].CompareTag("Player")) //can hurl fireball at player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator BiteTimer()
    {
        canBite = false;
        Bite();
        yield return new WaitForSeconds(2f);
        canBite = true;
    }

    private IEnumerator FireballTimer()
    {
        canFireball = false;
        ThrowFireball();
        yield return new WaitForSeconds(4f);
        canFireball = true;
    }
    private IEnumerator FireballDestroyTimer(GameObject fireball)
    {
        yield return new WaitForSeconds(2f);
        Destroy(fireball);
    }
}    
