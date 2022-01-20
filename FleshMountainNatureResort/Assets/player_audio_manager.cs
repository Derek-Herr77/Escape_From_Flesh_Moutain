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

    public void play_headshot_crack()
    {
        StartCoroutine(hit_delay());
        IEnumerator hit_delay()
        {
            yield return new WaitForSeconds(0.1f);
            sounds.PlayOneShot(headshot_crack);
        }
    }
}
