using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/13/2024
*  Script Description: Handles the Subject class that is apart of the Observer Pattern
*/

public abstract class Subject : MonoBehaviour
{
    private readonly ArrayList observers = new ArrayList();

    protected void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    protected void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void Notify()
    {
        foreach (IObserver observer in observers)
            observer.Notify(this);
    }
}
