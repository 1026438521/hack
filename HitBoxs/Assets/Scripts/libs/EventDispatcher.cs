
using UnityEngine;
using System.Collections.Generic;

public class EventDispatcher {

    public static EventDispatcher Instance;

    public delegate void EventHandler(object data);

    private Dictionary<string, EventHandler> eventList = new Dictionary<string, EventHandler>();

    static EventDispatcher()
    {
		Instance = new EventDispatcher();
    }

    private EventDispatcher() { }

    public void AddEventListener(string eventName,EventHandler handler)
    {
        if (eventList.ContainsKey(eventName))
        {
            eventList[eventName] += handler;
        }
        else
        {
            eventList.Add(eventName, handler);
        }
    }

    public void RemoveEventListener(string eventName,EventHandler handler)
    {
        if (eventList.ContainsKey(eventName))
        {
            eventList[eventName] -= handler;
            if (eventList[eventName] == null)
            {
                eventList.Remove(eventName);
            }
        }
        else
        {
            Debugger.Log("EventDispatch Warnning... [" + eventName + "] is already Remove!");
        }
    }

    public void RemoveEventListenerAll(string eventName)
    {
        if (eventList.ContainsKey(eventName))
        {
            eventList.Remove(eventName);
        }
    }

    public bool InvokeEvent(string eventName, object data = null)
    {
        if (eventList.ContainsKey(eventName))
        {
            eventList[eventName].Invoke(data);

            return true;
        }
        return false;
    }

    public void Clear()
    {
        eventList.Clear();
    }
}
