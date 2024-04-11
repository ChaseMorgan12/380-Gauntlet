using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles exiting the level
*/

public class Exit : MonoBehaviour
{
    public static Action OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnExit?.Invoke();
    }
}
