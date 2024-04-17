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

    private Vector2 moveValue;
    private Command _attack1, _attack2, _attack3;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }  

        _attack1 = new Attack1(GetComponent<BasePlayer>());
        _attack2 = new Attack2(GetComponent<BasePlayer>());
        _attack3 = new Attack3(GetComponent<BasePlayer>());
    }

    private void Update()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(moveValue.x, 0.0f, moveValue.y) * playerSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
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
