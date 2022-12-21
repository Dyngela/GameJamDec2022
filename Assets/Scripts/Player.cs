using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [Header("Player Properties")]
    // GameObject
    public static Player instance;

    public Animator animator;

    [Header("Component refs")]
    [SerializeField] private Slider SanitySliderRef;

    [Header("Stats")]
    // Stats
    public float maxSanity = 100f;
    public float sanity;
    [SerializeField] private float sanityLoseRate = .5f;

    // Inventory
    private bool _hasFirstAntidoteComponent;
    private bool _hasSecondAntidoteComponent;
    private bool _hasThirdAntidoteComponent;
    public bool playerCured;
    
    // Animation keys
    [Header("Animation")]
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] private Transform renderTransform;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (GameManager.instance.isGamePaused) return;
        
        HandleSanity();
        
        // Movement
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        
        // Animations
        animator.SetFloat(Horizontal, inputMovement.x);
        animator.SetFloat(Vertical, inputMovement.y);
        animator.SetFloat(Speed, inputMovement.sqrMagnitude);
        if (inputMovement.x > 0)
            renderTransform.localScale = new Vector3(1, 1, 1);
        if (inputMovement.x < 0)
            renderTransform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleSanity()
    {
        sanity -= sanityLoseRate * Time.deltaTime;
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

    public void Reset()
    {
        transform.position = Vector3.zero;
        sanity = maxSanity;
    }
}
