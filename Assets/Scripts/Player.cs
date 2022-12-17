using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    // GameObject
    public static Player instance;
    public Rigidbody2D rb;
    
    // Stats
    private Vector2 _movement;
    public float maxHealth = 100f;
    public float health;
    public float moveSpeed = 5f;
    public float maxSanity = 100f;
    public float sanity;
    private const float SANITY_LOSE_RATE = 2.0f;
    public float maxStamina = 100f;
    public float stamina;

    // Inventory
    private bool _hasFirstAntidoteComponent;
    private bool _hasSecondAntidoteComponent;
    private bool _hasThirdAntidoteComponent;
    public bool playerCured;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        sanity = maxSanity;
        stamina = maxStamina;
    }

    private void Update()
    {
        if (GameManager.instance.isGamePaused) return;
        
        HandleSanity();
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        // todo check if the position is accessible
        rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void HandleSanity()
    {
        sanity -= SANITY_LOSE_RATE * Time.deltaTime;
            //sanitySlider.value = sanity;
    }

    private void RegainSanity(float sanityGain)
    {
        sanity += sanityGain;
        if (sanity > 100)
        {
            sanity = 100;
        }
        //sanitySlider.value = sanity;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        //healthSlider.value = health;
    }
    
}
