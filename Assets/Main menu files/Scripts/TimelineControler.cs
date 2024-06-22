using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControler : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public void Play()
    {
        // Debug.Log("I was attempted to play");
        playableDirector.Play();
    }

    public float PlayDuration()
    {
        Debug.Log($"{playableDirector.duration} is the duration");
        return (float)playableDirector.duration;
    }
}
