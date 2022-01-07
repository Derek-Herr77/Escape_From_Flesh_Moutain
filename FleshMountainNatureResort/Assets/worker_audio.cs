using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worker_audio : MonoBehaviour
{
    public AudioSource audio1_norm;
    public AudioSource audio2_attack_overlay;
    public AudioSource audio3_audio_spikes;
    public AudioClip   alert, attack, death, hit;


    private void Start()
    {
        audio2_attack_overlay.enabled = false;
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
    public void play_death_noise()
    {
        if(gameObject.active == true)
        {
            StartCoroutine(kill_ai());
        }
        IEnumerator kill_ai()
        {
            audio1_norm.enabled = false;
            audio3_audio_spikes.PlayOneShot(death);
            stop_chase_noise();
            yield return new WaitForSeconds(3f);
            disable_noises();
        }
    }
    public void play_hit_noise()
    {
        audio3_audio_spikes.PlayOneShot(hit);
    }
    public void disable_noises()
    {
        gameObject.SetActive(false);
    }
}
