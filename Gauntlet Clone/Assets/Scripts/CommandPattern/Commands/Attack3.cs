/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/16/2024
*  Script Description: Handles the melee attack of the player
*/

public class Attack3 : Command
{
    private BasePlayer _player;

    public Attack3(BasePlayer player)
    {
        _player = player;
    }

    public override void Execute()
    {
        _player.Attack3();
    }
}
