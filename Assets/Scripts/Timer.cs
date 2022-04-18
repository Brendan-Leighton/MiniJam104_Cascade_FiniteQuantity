using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer
{
    public Timer(TMP_Text uiElement, float startTime)
    {
        UI_Timer_Text = uiElement;
        _startingTime = startTime;
        Time = startTime;
    }

    // PROPERTIES
    private float _time;
    public float Time
    {
        get => _time;
        set
        {
            if (value > 0) _time = value;
            else _time = 0;
        }
    }

    // FIELDS
    public TMP_Text UI_Timer_Text;
    private float _startingTime;
    public bool isStarted = false;

    // LIFE CYCLE METHODS
    public void Update()
    {
        //Debug.Log("Timer.cs > Update(): has ran");
        if (isStarted)
        {
            // stop timer
            if (_time <= 0)
            {
                Debug.Log("Stopping timer... ");
                isStarted = false;
            }
            // count down
            if (_time > 0)
            {
                //Debug.Log("Timer going");
                _time -= UnityEngine.Time.deltaTime;

                //Debug.Log("Timer.cs > Update() > _time: " + _time);

                UI_Timer_Text.text = "Time: " + _time.ToString("#0.00");
            }
        }
    }

    // CLASS METHODS
    public void StartTimer()
    {
        Time = _startingTime;
        isStarted = true;
    }
    public float StopTimer()
    {
        isStarted = false;
        return Time;
    }
}

