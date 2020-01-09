using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//allows events to be triggered and listened for across the game
public class EventManager : MonoBehaviour
{
    private Dictionary<string, GameObjectEvent> eventDictionary;

    private static EventManager eventManager;

    //a unityevent that also allows a gameobject parameter to be passed
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    { }

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in the scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, GameObjectEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<GameObject> listener)
    {
        GameObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameObjectEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<GameObject> listener)
    {
        if (eventManager == null) return;
        GameObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, GameObject testObject)
    {
        GameObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(testObject);
        }
    }
}
