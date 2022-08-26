using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSurface : MonoBehaviour
{
    [SerializeField]
    float bounceStrength = 1.2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // post event that collision with object has occured for sound or visual updates
        // Post.notificaiton(this,collision);

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * bounceStrength);
        }

    }
}
