using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource sfxPlayer;

    [SerializeField] private float fadeDuration;

    private AudioClip currMusic;
    private float originalMusicVol;

    [SerializeField] List<AudioData> sfxList;
    Dictionary<AudioId, AudioData> sfxLookup;

    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        instance = this;
    }

    private void Start()
    {
        originalMusicVol = musicPlayer.volume;

        sfxLookup = sfxList.ToDictionary(x => x.id);
    }

    public void PlaySfx(AudioClip clip, bool pauseMusic = false)
    {
        if (clip == null) return;

        if (pauseMusic)
        {
            musicPlayer.Pause();
            StartCoroutine(UnPauseMusic(clip.length/2));
        }

        sfxPlayer.PlayOneShot(clip);
    }

    public void PlaySfx(AudioId audioId, bool pauseMusic = false)
    {
        if (!sfxLookup.ContainsKey(audioId)) return;

        var audioData = sfxLookup[audioId];
        PlaySfx(audioData.clip, pauseMusic);
    }

    public void PlayMusic(AudioClip clip, bool loop = true, bool fade = false)
    {
        if (clip == null || clip == currMusic) return;

        currMusic = clip;
        StartCoroutine(PlayMusicAsync(clip, loop, fade));
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    IEnumerator PlayMusicAsync(AudioClip clip, bool loop, bool fade)
    {
        if (fade)
            yield return musicPlayer.DOFade(0, fadeDuration).WaitForCompletion();

        musicPlayer.clip = clip;
        musicPlayer.loop = loop;
        musicPlayer.Play();

        if (fade)
            yield return musicPlayer.DOFade(originalMusicVol, fadeDuration).WaitForCompletion();
    }

    IEnumerator UnPauseMusic(float delay)
    {
        yield return new WaitForSeconds(delay);

        musicPlayer.volume = 0;
        musicPlayer.UnPause();
        musicPlayer.DOFade(originalMusicVol, fadeDuration);
    }
}

public enum AudioId { UISelect, Hit, Faint, ExpGain, ItemObtained, PokemonObtained }


[System.Serializable]
public class AudioData
{
    public AudioId id;
    public AudioClip clip;
}
