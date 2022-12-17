using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Properties")]
    public Vector2 inputMovement = new Vector2();
    public float moveSpeed = 5f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovements();
    }

    private void HandleMovements()
    {
        _rb.MovePosition(_rb.position + inputMovement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }
}
