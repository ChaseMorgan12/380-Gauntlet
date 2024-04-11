using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public class PlayerManager : Singleton<PlayerManager>
{
    public BasePlayer[] player = new BasePlayer[4];

    public event Action OnPlayerConnected;
}
