using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_audio_manager : MonoBehaviour
{
    private AudioSource sounds;
    public AudioClip headshot_crack;

    private void Start()
    {
        sounds = gameObject.GetComponent<AudioSource>();
    }

}
