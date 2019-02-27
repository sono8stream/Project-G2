using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubject : MonoBehaviour
{
    EventObserver[] observers;

    public void Subscribe(EventType eventType, int x, int y)
    {

    }
}

public enum EventType
{
    Manual, Auto, Async, PlayerTouch, EventTouch
}
