using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/04/20024
*  Script Description: Handles behavior for the Socerer Enemy
*/

public class Sorcerer : BaseEnemy
{
    //Initializing unique enemy variables////////
    public GameObject SorcererSpell;

    private float playerMeleeRange = 2f;

    private float spellRange = 15f;
    private float spellVelocity = 10f;
    private bool canCastSpell = true;
    private bool canTurnInvisible = true; 
    private bool isInvisible = false; //Shots will pass through them while invisible, won't be damaged, reference for player projectiles

    private void Awake()
    {
        damage = 300;
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInMeleeRange())
        {
            //Do nothing
        }
        else if (PlayerInSpellRange())
        {
            //try to turn invisible (1 in 500 chance but fires every fixedupdate)
            if (canTurnInvisible)
            {
                if (Random.Range(0,500) == 1)
                {
                    StartCoroutine(InvisibleTimer());
                }
            }
            //Shoot spell
            if (canCastSpell)
            {
                StartCoroutine(SpellTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private bool PlayerInMeleeRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerMeleeRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //will do nothign if player is found in range
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(_player.transform);
                return true;
            }
        }
        return false;
    }
    private void FireSpell()
    {
        //instansiate Spell prefab towards player
        Debug.Log("Sorcerer Casting spell");
        GameObject spellProj = Instantiate(SorcererSpell, transform.position, transform.rotation);
        spellProj.GetComponent<Rigidbody>().velocity = transform.forward * spellVelocity;
        StartCoroutine(SpellDestroyTimer(spellProj));
    }
    private bool PlayerInSpellRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spellRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can cast spell towards player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator SpellTimer()
    {
        canCastSpell = false;
        FireSpell();
        yield return new WaitForSeconds(2f);
        canCastSpell = true;
    }
    private IEnumerator SpellDestroyTimer(GameObject spellProj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(spellProj);
    }
    private IEnumerator InvisibleTimer()
    {
        canTurnInvisible = false;
        GetComponent<MeshRenderer>().material.color -= new Color(0, 0, 0, .4f);
        isInvisible = true;
        yield return new WaitForSeconds(3f);

        //Turn back visible
        canTurnInvisible = true;
        GetComponent<MeshRenderer>().material.color += new Color(0, 0, 0, .4f);
        isInvisible = false;
    }
}
