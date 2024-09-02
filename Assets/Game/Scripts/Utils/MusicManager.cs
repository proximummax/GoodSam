using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    public static float MusicVolume = 1;

    private AudioSource currentAudioSource;

    [SerializeField] private float fadeDuration = 5f;
    [SerializeField] private float timeToStartUpperFade = 0f;
    [SerializeField] private float timeToStartDownFade = 0f;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        currentAudioSource = gameObject.GetComponent<AudioSource>();
        MusicVolume = PlayerPrefs.GetFloat("volume", 1.0f);

    }
    private void Update()
    {
        SetMusicVolume();
    }
    public void StopMusic()
    {
        if (currentAudioSource != null)
            currentAudioSource.mute = true;
    }
    public IEnumerator PlayMusic(AudioClip clip)
    {
        if (currentAudioSource.clip != null)
        {
            currentAudioSource.mute = false;
            while (currentAudioSource.isPlaying)
            {
                if (currentAudioSource.time <= instance.timeToStartUpperFade)
                {
                    yield return instance.StartCoroutine(StartFade(currentAudioSource, instance.fadeDuration, MusicVolume));
                }
                if (currentAudioSource.clip.length - currentAudioSource.time <= instance.timeToStartDownFade)
                {
                    yield return instance.StartCoroutine(StartFade(currentAudioSource, instance.fadeDuration, 0));
                    currentAudioSource.Stop();
                    currentAudioSource.clip = null;
                }
                yield return null;
            }
        }

        currentAudioSource.clip = clip;
        currentAudioSource.volume = 0;

        currentAudioSource.Play();
        while (currentAudioSource.isPlaying)
        {
            if (currentAudioSource.time <= instance.timeToStartUpperFade)
            {
                yield return instance.StartCoroutine(StartFade(currentAudioSource, MusicVolume <= 0.0f ? 0.0f : fadeDuration, MusicVolume));
            }
            if (currentAudioSource.clip.length - currentAudioSource.time <= timeToStartDownFade)
            {
                yield return instance.StartCoroutine(StartFade(currentAudioSource, fadeDuration, 0));
                currentAudioSource.Stop();
                currentAudioSource.clip = null;
            }
            yield return null;
        }
    }
    private IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    private void SetMusicVolume()
    {

        if (MusicVolume != currentAudioSource.volume && currentAudioSource.isPlaying)
            currentAudioSource.volume = MusicVolume;
    }
  
}
