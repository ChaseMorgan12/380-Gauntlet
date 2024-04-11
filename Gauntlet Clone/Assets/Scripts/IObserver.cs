using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Interface for observers
*/

public interface IObserver
{
    public void Notify(Subject subject);
}
