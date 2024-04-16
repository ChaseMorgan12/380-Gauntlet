using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;

    private Vector2 moveValue;

    private void Update()
    {
        transform.Translate(playerSpeed * Time.deltaTime * new Vector3(moveValue.x, 0, moveValue.y));
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        Debug.Log("Attack 1");
    }

    public void Attack2(InputAction.CallbackContext context)
    {
        Debug.Log("Attack 2");
    }

    public void Attack3(InputAction.CallbackContext context)
    {
        Debug.Log("Attack 3");
    }
}
