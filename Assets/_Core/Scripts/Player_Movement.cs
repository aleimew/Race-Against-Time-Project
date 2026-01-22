using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float acceleration = 15f;
    public float friction = 30f;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float defaultSpeed;

    private Vector3 startLocation;

    private bool movementLocked = false;

    [SerializeField] private Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        defaultSpeed = baseSpeed;
        startLocation = transform.position;

        animator = GetComponent<Animator>();
    }
    
    /// <summary>
    /// Sets the boolean in the animation script whether the player is dead or not
    /// </summary>
    /// <param name="playerDead">are they alive?</param>
    public void SetPlayerDied(bool playerDead)
    {
        animator.SetBool("PlayerDied", playerDead);
    }


    void Update()
    {
        if (movementLocked) return;

        HandleInput();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        if (movementLocked)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        ApplyMovement();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(x, y).normalized;
        Vector2 targetVelocity = inputDir * baseSpeed;

        if (inputDir.sqrMagnitude > 0.01f)
            velocity = Vector2.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);
        else
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, friction * Time.deltaTime);
    }

    void ApplyMovement()
    {
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Walls"))
        {
            GameManager.Instance.ResetPlayers();
        }
    }

    public void ResetPosition()
    {
        transform.position = startLocation;
        rb.velocity = Vector2.zero;
        velocity = Vector2.zero;
    }

    public void ChangeSpeed(float multiplier)
    {
        baseSpeed = defaultSpeed * multiplier;
    }

    public void ResetSpeed()
    {
        baseSpeed = defaultSpeed;
    }

    public void LockMovement()
    {
        movementLocked = true;
        rb.velocity = Vector2.zero;
        velocity = Vector2.zero;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }
}


