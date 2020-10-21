using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public TextMeshProUGUI eventText;
    public Event[] events;
    private System.Random r = new System.Random();
    private Event selectedEvent;
    private float clearTimer;

    public void AttemptEvent()
    {
        selectedEvent = events[r.Next(0, events.Length)];
        if (selectedEvent.RunEvent())
        {
            eventText.text = selectedEvent.description;
        }

        clearTimer = 5f;
    }

    private void Update()
    {
        clearTimer -= 1 * Time.deltaTime;
        if (clearTimer <= 0f)
        {
            clearTimer = 0f;
            eventText.text = string.Empty;
        }
    }
}
