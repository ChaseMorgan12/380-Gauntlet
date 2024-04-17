using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/16/2024
*  Script Description: Handles the main attack of the player
*/

public class Attack1 : Command
{
    private BasePlayer _player;

    public Attack1(BasePlayer player)
    {
        _player = player;
    }

    public override void Execute()
    {
       throw new System.NotImplementedException(GetType().Name + " has not been implemented yet");
    }
}
