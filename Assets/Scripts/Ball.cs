using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startingSpeed = 12.0f;

    Vector2 startPosition;

    Rigidbody2D rigidBody;

    private void Start()
    {
        startPosition = this.transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void AddRandomStartForce()
    {
        // Random float between 0 - 1 if below
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                         Random.Range(0.5f, 1.0f);
        Vector2 direction = new Vector2(x, y);
        rigidBody.AddForce(direction * startingSpeed);
    }

    public void AddForce(Vector2 force)
    {
        rigidBody.AddForce(force);
    }

    public void ResetPosition()
    {
        rigidBody.position = startPosition;
        rigidBody.velocity = Vector3.zero;
    }

}
