using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public abstract class Subject : MonoBehaviour
{
    private readonly ArrayList observers = new ArrayList();

    protected void Attach(IObserver observer)
    {

    }

    protected void Detach(IObserver observer)
    {

    }

    protected void Notify()
    {

    }
}
