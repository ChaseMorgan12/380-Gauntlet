/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/18/2024
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
        _player.Attack1();
    }
}
