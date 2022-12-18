using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [Header("Player Properties")]
    // GameObject
    public static Player instance;

    public Animator animator;

    [Header("Component refs")]
    [SerializeField] private Slider HealthSliderRef;
    [SerializeField] private Slider StaminaSliderRef;
    [SerializeField] private Slider SanitySliderRef;

    [Header("Stats")]
    // Stats
    public float maxHealth = 100f;
    public float health;
    public float maxSanity = 100f;
    public float sanity;
    private const float SANITY_LOSE_RATE = .5f;
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
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", inputMovement.x);
        animator.SetFloat("Vertical", inputMovement.y);
        animator.SetFloat("Speed", inputMovement.sqrMagnitude);
        if (inputMovement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (inputMovement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleSanity()
    {
        sanity -= SANITY_LOSE_RATE * Time.deltaTime;
        SanitySliderRef.value = sanity;
    }

    private void RegainSanity(float sanityGain)
    {
        sanity += sanityGain;
        if (sanity > 100)
        {
            sanity = 100;
        }
        SanitySliderRef.value = sanity;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        HealthSliderRef.value = health;
    }
    
}
