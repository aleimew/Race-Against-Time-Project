using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Player_Movement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float acceleration = 15f;
    public float friction = 30f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(x, y).normalized;

        Vector2 targetVelocity = inputDir * baseSpeed;

        if (inputDir.sqrMagnitude > 0.01f)
        {
            velocity = Vector2.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, friction * Time.deltaTime);
        }
    }

    void ApplyMovement()
    {
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Walls")
        {
            GameManager.Instance.ReloadCurrentScene();
        }
    }
}
