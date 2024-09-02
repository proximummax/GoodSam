using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusics : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;

    private void Start()
    {
        StartCoroutine(StartMusic());
        if (SceneManager.GetActiveScene().buildIndex == 0 && MusicManager.MusicVolume != 0.0f) 
        {
            MusicManager.MusicVolume = 1.0f;
        }
        else
        {
            if (MusicManager.MusicVolume != 0.0f)
                MusicManager.MusicVolume = 0.5f;
        }
        
    }


    private IEnumerator StartMusic()
    {
        while (true)
        {
            foreach (var clip in audioClips)
            {
                yield return StartCoroutine(MusicManager.instance.PlayMusic(clip));
            }
            yield return null;
        }
    }
}
