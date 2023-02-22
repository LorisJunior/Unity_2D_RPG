using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManager", menuName = "Scriptable Objects/Audio/Audio Manager")]
public class SO_AudioManager : ScriptableObject
{
    public List<AudioClip> audioClips;

    public AudioClip GetAudioClip(int i)
    {
        return audioClips[i];
    }
}