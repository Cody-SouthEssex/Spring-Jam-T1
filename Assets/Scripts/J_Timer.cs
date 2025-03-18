using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class J_Timer
{
    [HideInInspector] public float startTime;
    [HideInInspector] public float endTime;
    public float duration;
    [HideInInspector] public bool isComplete;

    public J_Timer(float duration)
    {
        this.duration = duration;
    }

    public void Start()
    {
        startTime = Time.time;
        endTime = startTime + duration;
        isComplete = false;
    }

    public void Start(float duration)
    {
        this.duration = duration;

        Start();
    }

    public bool IsComplete()
    {
        return Time.time >= endTime;
    }

    public bool OnCompletion()
    {
        if (!isComplete)
        {
            if (IsComplete())
            {
                isComplete = true;
                return true;
            }
        }
        return false;
    }

    public float RemainingTime()
    {
        return endTime - Time.time;
    }
    public float RemainingPercent()
    {
        return RemainingTime() / duration;
    }
}
