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

        transform.LookAt(transform.position + _rb.velocity.normalized);
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
