using System.Collections.Generic;

using UnityEngine;

public class EventDispacher
{
    private static List<TopicListeners> topicListeners = new List<TopicListeners>();

    private static List<EventListener> globalListeners = new List<EventListener>();

    public static void Subscribe(EventListener eventListener, EventTopic topic, bool isOneShot)
    {
        if (eventListener == null || topic.Equals(EventTopic.Count))
            return;

        if (topicListeners == null || topicListeners.Count == 0)
            CreateTopicListeners();

        for (int i = 0; i < topicListeners.Count; i++)
        {
            if (topicListeners[i].topic.Equals(topic))
            {
                ListenerData listener = new ListenerData(eventListener, isOneShot);
                topicListeners[i].RegisterEvent(listener);
                break;
            }
        }
    }

    public static void SubscribeToGlobal(EventListener eventListener)
    {
        if (eventListener == null)
            return;

        globalListeners.Add(eventListener);
    }

    private static void CreateTopicListeners()
    {
        for (int i = 0; i < (int)EventTopic.Count; i++)
        {
            TopicListeners listeners = new TopicListeners((EventTopic)i);
            topicListeners.Add(listeners);
        }
    }

    public static void FireEvent(EventTopic topic)
    {
        for (int i = 0; i < topicListeners.Count; i++)
        {
            if (topicListeners[i].topic.Equals(topic))
            {
                topicListeners[i].FireEvent();
                NotifyGlobals(topic);
                return;
            }
        }

        NotifyGlobals(topic);
    }

    private static void NotifyGlobals(EventTopic topic)
    {
        for (int i = 0; i < globalListeners.Count; i++)
        {
            if (globalListeners[i] != null)
            {
                globalListeners[i].TriggerEvent(topic);
            }
            else
            {
                globalListeners.RemoveAt(i);
                i--;
            }
        }
    }
}

public struct TopicListeners
{
    public EventTopic topic;

    private List<ListenerData> listeners;

    public TopicListeners(EventTopic topic)
    {
        this.topic = topic;
        listeners = new List<ListenerData>();
    }

    public void RegisterEvent(ListenerData newListener)
    {
        listeners.Add(newListener);
    }

    private bool isListenerRegistered(EventListener newEventListener)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            if (!listeners[i].HasEvent() &&
                listeners[i].eventListener.Equals(newEventListener))
            {
                return true;
            }
        }

        return false;
    }

    public void FireEvent()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].HasEvent())
            {
                listeners[i].FireEvent(topic);
            }

            if (!listeners[i].HasEvent() || listeners[i].isOneShot())
            {
                listeners.RemoveAt(i);
                i--;
            }
        }
    }
}

public struct ListenerData
{
    public EventListener eventListener;
    private bool oneShot;

    public ListenerData(EventListener eventListener, bool oneShot)
    {
        this.eventListener = eventListener;
        this.oneShot = oneShot;
    }

    public void FireEvent(EventTopic topic)
    {
        eventListener.TriggerEvent(topic);
    }

    public bool HasEvent()
    {
        return eventListener != null;
    }

    public bool isOneShot()
    {
        return oneShot;
    }
}

public interface EventListener
{
    void TriggerEvent(EventTopic topicDispached);
}