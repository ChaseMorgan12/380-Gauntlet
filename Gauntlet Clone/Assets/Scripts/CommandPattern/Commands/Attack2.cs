using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/16/2024
*  Script Description: Handles the secondary attack of the player
*/

public class Attack2 : Command
{
    private BasePlayer _player;

    public Attack2(BasePlayer player)
    {
        _player = player;
    }

    public override void Execute()
    {
       throw new System.NotImplementedException(GetType().Name + " has not been implemented yet");
    }
}
