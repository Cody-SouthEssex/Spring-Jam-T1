using System.Collections;

using System.Collections.Generic;

using UnityEditor;

using UnityEngine;



[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class SoundSystem : MonoBehaviour
{

    [System.Serializable]
    public class Sound
    {
        public AudioClip audioClip;
        public int weight;
    }

    public List<Sound> sounds = new List<Sound>();
    private AudioSource audioSource;
    private float basePitch;
    public float pitchRange;

    public bool playOnAwake;
    public bool overrideSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        basePitch = audioSource.pitch;

        if (playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (!overrideSound)
        {
            if (audioSource.isPlaying)
            {
                return;
            }
        }

        Sound chosenSound = PickSound();

        audioSource.pitch = basePitch + Random.Range(-pitchRange, pitchRange);

        audioSource.PlayOneShot(chosenSound.audioClip);
    }

    private Sound PickSound()
    {
        int total = 0;

        foreach (Sound sound in sounds)
        {
            total += sound.weight;
        }

        int value = Random.Range(0, total);

        total = 0;

        foreach (Sound sound in sounds)
        {
            total += sound.weight;

            if (value <= total)
            {
                return sound;
            }
        }

        return null;
    }
}

