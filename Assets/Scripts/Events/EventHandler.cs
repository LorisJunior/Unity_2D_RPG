using System;
using UnityEngine;

public static class EventHandler
{
    public static Action<AudioClip> PlaySoundEvent;
    public static Action<int> IncreaseCoinEvent;
    public static Action<int> ChangeHealthEvent;

    public static void CallPlaySoundEvent(AudioClip audioClip)
    {
        if (PlaySoundEvent != null)
        {
            PlaySoundEvent(audioClip);
        }
    }

    public static void CallIncreaseCoinEvent(int amount)
    {
        if (IncreaseCoinEvent != null)
        {
            IncreaseCoinEvent(amount);
        }
    }

    public static void CallChangeHealthEvent(int amount)
    {
        if (ChangeHealthEvent != null)
        {
            ChangeHealthEvent(amount);
        }
    }
}