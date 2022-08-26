using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Goal : MonoBehaviour
{
    public EventTrigger.TriggerEvent score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // post event that collision with object has occured for sound or visual updates
        // Post.notificaiton(this,collision);

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            // Trigger all events attached in editor.
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            score.Invoke(eventData);
        }
    }
}
