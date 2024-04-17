using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/16/2024
*  Script Description: Basic invoker for commands (Just want to try and decouple player controls)
*/

public class Invoker : MonoBehaviour
{
    public void ExecuteCommand(Command command)
    {
        command.Execute();

        Debug.Log("Executed Command: " + command.GetType().Name);
    }
}
