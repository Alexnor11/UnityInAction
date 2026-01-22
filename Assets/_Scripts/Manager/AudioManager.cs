using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {  get; private set; }

    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource music1Source;
    [SerializeField] AudioSource Music2Source;
    
    [SerializeField] string introBGMusic;
    [SerializeField] string levelBGMusic;

    private NetworkService network;

    private AudioSource activMusic;
    private AudioSource inactiveMusic;

    private float crossFadeRate = 1.6f;
    private bool crossFading;

    private float _musicVolume;
    public float musicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            if (music1Source != null)
            {
                music1Source.volume = _musicVolume;
                Music2Source.volume = _musicVolume;
            }
        }
    }
    
    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public bool musicMute
    {
        get
        {
            if(music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }
        set
        {
            if(music1Source == null)
            {
                music1Source.mute = value;
                Music2Source.mute = value;
            }
        }
    }

    public void Startup(NetworkService service)
    {
        network = service;

        music1Source.ignoreListenerVolume = true;
        Music2Source.ignoreListenerVolume= true;
        music1Source.ignoreListenerPause = true;
        Music2Source.ignoreListenerPause= true;

        soundVolume = 1f;
        musicVolume = 1f;

        activMusic = music1Source;
        inactiveMusic = Music2Source;

        status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load($"Music/{introBGMusic}") as AudioClip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load($"Music/{levelBGMusic}") as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if(crossFading) { return; }
        StartCoroutine(CrossFadeMusic(clip));
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        crossFading = true;

        inactiveMusic.clip = clip;
        inactiveMusic.volume = 0;
        inactiveMusic.Play();

        float scaleRate = crossFadeRate * musicVolume;
        while (activMusic.volume > 0)
        {
            activMusic.volume -= scaleRate * Time.deltaTime;
            inactiveMusic.volume += scaleRate * Time.deltaTime;

            yield return null;
        }
        AudioSource temp = activMusic;

        activMusic = inactiveMusic;
        activMusic.volume = musicVolume;

        inactiveMusic = temp;
        inactiveMusic.Stop();

        crossFading = false;
    }

    public void StopMusic()
    {
        activMusic.Stop();
        inactiveMusic.Stop();
    }
}
