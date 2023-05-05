using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource woodImpactSound;
    public AudioSource barOpenSound;
    public AudioSource flapSoundSound;
    public AudioSource paperSound;
    public AudioSource music;
    public AudioSource deathSound;

    public static AudioManager instance;

    public float minTimeBetweenSounds = 0.7f;

    private bool isPlayingSound = false;
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayWoodImpactSound()
    {
        if (!isPlayingSound)
        {
            StartCoroutine(PlaySoundCoroutine(woodImpactSound));
        }
    }

    public void PlayBarOpenSound()
    {
        barOpenSound.Play();
        Debug.Log("gg");
    }

    public void PlayFlapSound()
    {
        flapSoundSound.Play();
    }

    public void PlayPaperSound()
    {
        paperSound.Play();
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    private IEnumerator PlaySoundCoroutine(AudioSource audio)
    {
        isPlayingSound = true;
        audio.Play();

        yield return new WaitForSeconds(minTimeBetweenSounds);

        isPlayingSound = false;
    }
}
