using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Properties")]
    public Vector2 inputMovement = new Vector2();
    public float moveSpeed = 5f;

    public Rigidbody2D _rb;
    

    private void FixedUpdate()
    {
        HandleMovements();
    }

    private void HandleMovements()
    {
        _rb.MovePosition(_rb.position + inputMovement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }
}
