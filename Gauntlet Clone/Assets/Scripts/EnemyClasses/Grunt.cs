using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 04/30/2024
*  Script Description:
*/

public class Grunt : BaseEnemy
{
    private float clubRange = 1f;
    private bool canSwingClub = true;

    private void Awake()
    {
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
                StartCoroutine(clubTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void SwingClub()
    {
        //Swing club at player if in club range
        Debug.Log("Swinging Club");

    }
    private bool PlayerInClubRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, clubRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can swing club at player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator clubTimer()
    {
        canSwingClub = false;
        SwingClub();
        yield return new WaitForSeconds(2f);
        canSwingClub = true;
    }

}
