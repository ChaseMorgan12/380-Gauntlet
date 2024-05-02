using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/02/20024
*  Script Description: Handles behavior for the Grunt Enemy
*/

public class Grunt : BaseEnemy
{
    public GameObject rock;

    private float clubRange = 1.7f;
    private bool canSwingClub = true;

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
        if (PlayerInClubRange())
        {
            if (canSwingClub)
            {
                StartCoroutine(SwingClubTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void SwingClub()
    {
        Debug.Log("Swinging Club");
    }
    private bool PlayerInClubRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, clubRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can swing club towards player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator SwingClubTimer()
    {
        canSwingClub = false;
        SwingClub();
        yield return new WaitForSeconds(2f);
        canSwingClub = true;
    }
    
}
