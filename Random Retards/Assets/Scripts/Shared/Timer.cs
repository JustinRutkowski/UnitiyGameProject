using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private class TimedEvent
    {
        public float TimeToExecute;
        public CallBack Method;
    }

    private List<TimedEvent> events;

    public delegate void CallBack();

    private void Awake()
    {
        events = new List<TimedEvent>();
    }

    public void Add(CallBack method, float inSeconds)
    {
        events.Add(new TimedEvent {
            Method = method,
            TimeToExecute = Time.time + inSeconds
        });
    }

    private void Update()
    {
        if (events.Count == 0)
            return;

        for (int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if (timedEvent.TimeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }
        }        
    }
}
