using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Movement
    Rigidbody2D rigidbody;
    Vector2 direciton = new Vector2();
    [SerializeField]
    float speed = 10.0f;

    // Reset
    Vector2 resetPosition;

    // Input
    [SerializeField]
    KeyCode upInput;
    [SerializeField]
    KeyCode downInput;


    
    // Ai
    public Rigidbody2D ball;
    [SerializeField]
    bool _aiControlled = true;
    public bool AIControlled
    {
        get
        {
            return _aiControlled;
        }
        set
        {
            _aiControlled = value;
        }
    }

    private void Awake()
    {
        resetPosition = this.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(_aiControlled)
        {
            GetAiInput();
        }
        else
        {
            GetPlayerInput();
        }
    }

    // Could be it's own class but for simplicity kept in paddle.
    private void GetAiInput()
    {
        // If the ball is moving towards the paddle move towards it.
        // If multiple balls could move towards the closest or fasteset moving instead.
        if(Vector2.Dot(ball.velocity, (ball.position - rigidbody.position)) < 0)
        {
            // Ball moving towards paddle.
            if(ball.position.y > transform.position.y)
            {
                direciton = Vector2.up;
            }
            else if(ball.position.y < transform.position.y)
            {
                direciton = Vector2.down;
            }
        }
        else
        {
            // If doesn't need to move towards ball will move towards start position(reset posiiton).
            // Should be something like an arrive behavior.
            if(resetPosition.y > transform.position.y)
            {
                direciton = Vector2.up;
            }
            else if(resetPosition.y < transform.position.y)
            {
                direciton = Vector2.down;
            }
        }
    }

    private void GetPlayerInput()
    {

        if(Input.GetKey(upInput))
        {
            direciton = Vector2.up;
        }
        else if(Input.GetKey(downInput))
        {
            direciton = Vector2.down;
        }
        else
        {
            direciton = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if(direciton.magnitude != 0)
        {
            rigidbody.AddForce(direciton * speed);
        }
    }

    public void ResetPosition()
    {
        this.transform.position = resetPosition;
        rigidbody.velocity = Vector2.zero;
    }

}
