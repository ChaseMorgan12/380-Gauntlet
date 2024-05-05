using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/03/2024
*  Script Description: Handles exiting the level
*/

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    public static event Action<GameObject> OnExit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("Player"))
            OnExit?.Invoke(nextLevel);
    }
}
