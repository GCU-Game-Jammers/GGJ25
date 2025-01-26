using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] sounds;

    [SerializeField] private AudioSource sfxSource;
    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(string sfx)
    {
        AudioClip s = System.Array.Find(sounds, sound => sound.name == sfx);
        sfxSource.clip = s;
        sfxSource.Play();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public bool CheckPlaying()
    {
        return sfxSource.isPlaying;
    }

}
