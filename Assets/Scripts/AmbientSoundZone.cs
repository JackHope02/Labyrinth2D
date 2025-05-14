using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AmbientSoundZone : MonoBehaviour
{
    public AudioClip[] soundVariants;
    public float intervalMin = 10f;
    public float intervalMax = 20f;
    public float initialDelay = 0f;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomLoop());
    }

    IEnumerator PlayRandomLoop()
    {
        if (initialDelay > 0f)
            yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            PlayRandomSound();
            float waitTime = Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void PlayRandomSound()
    {
        if (soundVariants.Length == 0) return;

        AudioClip clip = soundVariants[Random.Range(0, soundVariants.Length)];
        source.clip = clip;
        source.Play();
    }
}
