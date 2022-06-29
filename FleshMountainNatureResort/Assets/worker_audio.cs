using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worker_audio : MonoBehaviour
{
    public AudioSource audio1_norm;
    public AudioSource audio2_attack_overlay;
    public AudioSource audio3_audio_spikes;
    public AudioClip   alert, attack, death, hit;
    private float startTime;


    private void Start()
    {
        audio2_attack_overlay.enabled = false;
        audio2_attack_overlay.pitch = Random.Range(0.6f, 1);
        audio3_audio_spikes.pitch = Random.Range(0.6f, 1);
    }

    private void PlayOneShot(AudioClip clip, float volume)
    {
        audio3_audio_spikes.PlayOneShot(clip, volume);
        startTime = Time.time;
    }

    public void play_attack_noise()
    {
        audio3_audio_spikes.PlayOneShot(attack);
    }
    public void play_chase_noise()
    {
        StartCoroutine(chase_sounds());
        IEnumerator chase_sounds()
        {
            audio3_audio_spikes.PlayOneShot(alert);
            yield return new WaitForSeconds(2f);
            audio2_attack_overlay.enabled = true;
        }
    }
    public void stop_chase_noise()
    {
        audio2_attack_overlay.enabled = false;
    }
    public void play_death_noise(bool headshot)
    {
        if(gameObject.active == true)
        {
            StartCoroutine(kill_ai());
        }
        IEnumerator kill_ai()
        {
            audio1_norm.enabled = false;
            stop_chase_noise();
            audio3_audio_spikes.pitch = Random.Range(0.4f, 1f);
            if (!headshot) { audio3_audio_spikes.PlayOneShot(death); }
            StartCoroutine(FadeOut(audio3_audio_spikes, 4f));
            yield return new WaitForSeconds(4f);
            disable_noises();
        }
    }
    public void play_hit_noise()
    {
        if(!isPlaying(hit))
        {
            PlayOneShot(hit, 1);
        }
    }
    public void disable_noises()
    {
        gameObject.SetActive(false);
    }


    //fade_out a noise


    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public bool isPlaying(AudioClip clip)
    {
        if ((Time.time - startTime) >= clip.length)
            return false;
        return true;
    }
}
