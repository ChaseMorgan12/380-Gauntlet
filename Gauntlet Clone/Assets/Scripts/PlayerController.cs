using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/16/2024
*  Script Description: This script pretty much only gets input besides movement
*/

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;

    private Rigidbody _rb;

    private Vector2 moveValue;
    private Command _attack1, _attack2, _attack3;
    private bool _ignoreNextInput = false;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
            _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(moveValue.x, 0, moveValue.y) * playerSpeed, ForceMode.Impulse);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();

        if (context.performed)
        {
            if (_ignoreNextInput)
            {
                _ignoreNextInput = false;
                return;
            }

            switch (moveValue) //This sucks, and works...
            {
                case Vector2 when moveValue == Vector2.up:
                    //Debug.Log("Forward");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Vector2 when moveValue == Vector2.down:
                    //Debug.Log("Backward");
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Vector2 when moveValue == Vector2.right:
                    //Debug.Log("Right");
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Vector2 when moveValue == Vector2.left:
                    //Debug.Log("Left");
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
                case Vector2 when moveValue.normalized == new Vector2(Mathf.Sqrt(.5f), Mathf.Sqrt(.5f)):
                    //Debug.Log("Angled Right");
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    _ignoreNextInput = true; //Ignore next input because there are two buttons pressed at the same time which leads to this firing twice
                    break;
                case Vector2 when moveValue.normalized == new Vector2(Mathf.Sqrt(.5f), -Mathf.Sqrt(.5f)):
                    //Debug.Log("Angled Negative Right");
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    _ignoreNextInput = true;
                    break;
                case Vector2 when moveValue.normalized == new Vector2(-Mathf.Sqrt(.5f), Mathf.Sqrt(.5f)):
                    // Debug.Log("Angled Left");
                    transform.rotation = Quaternion.Euler(0, -45, 0);
                    _ignoreNextInput = true;
                    break;
                case Vector2 when moveValue.normalized == new Vector2(-Mathf.Sqrt(.5f), -Mathf.Sqrt(.5f)):
                    //Debug.Log("Angled Negative Left");
                    transform.rotation = Quaternion.Euler(0, -135, 0);
                    _ignoreNextInput = true;
                    break;
                default:
                    Debug.LogWarning("No movement direction matched the input, ignoring"); //Just in case (there really should never be a situation where this happens, unless we f up)
                    return;
            }
        }
    }

    public void SetupCommands()
    {
        BasePlayer player = GetComponent<BasePlayer>();
        //Debug.Log(player.GetType().ToString());
        _attack1 = new Attack1(player); //Command pattern my beloved <3
        _attack2 = new Attack2(player);
        _attack3 = new Attack3(player);
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.performed)
            _attack1.Execute();
    }

    public void Attack2(InputAction.CallbackContext context)
    {
        if (context.performed)
            _attack2.Execute();
    }

    public void Attack3(InputAction.CallbackContext context)
    {
        if (context.performed)
            _attack3.Execute();
    }
}
