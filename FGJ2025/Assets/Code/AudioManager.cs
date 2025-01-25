using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    AudioSource _musicAS;
    AudioSource _soundAS;

    public AudioMixer masterMixer;

    private float _masterFloat = 1f, _musicFloat = 1f, _soundFloat = 1f;

    public float MasterFloat { get { return _masterFloat; } }
    public float MusicFloat { get { return _musicFloat; } }
    public float SoundFloat { get { return _soundFloat; } }

    public float MasterVolume
    {
        get {
            float returnFloat;
            masterMixer.GetFloat("MasterVol", out returnFloat);
            returnFloat = Mathf.Log10(returnFloat) * 20;
            return returnFloat;
        }
        set
        {
            float newValue = value;
            newValue = Mathf.Clamp(newValue, 0.0001f, 1f);
            masterMixer.SetFloat("MasterVol", Mathf.Log10(newValue) * 20);
            _masterFloat = newValue;
        }
    }

    public float MusicVolume
    {
        get {
            float returnFloat;
            masterMixer.GetFloat("MusicVol", out returnFloat);
            returnFloat = Mathf.Log10(returnFloat) * 20;
            return returnFloat;
        }
        set
        {
            float newValue = value;
            newValue = Mathf.Clamp(newValue, 0.0001f, 1f);
            masterMixer.SetFloat("MusicVol", Mathf.Log10(newValue) * 20);
            _musicFloat = newValue;
        }
    }

    public float SoundVolume
    {
        get {
            float returnFloat;
            masterMixer.GetFloat("SoundVol", out returnFloat);
            returnFloat = Mathf.Log10(returnFloat) * 20;
            return returnFloat;
        }
        set
        {
            float newValue = value;
            newValue = Mathf.Clamp(newValue, 0.0001f, 1f);
            masterMixer.SetFloat("SoundVol", Mathf.Log10(newValue) * 20);
            _soundFloat = newValue;
        }
    }

    void Awake()
    {
        // Singleton
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        // Get AudioSources from children
        foreach(Transform t in transform)
        {
            if(t.name.ToLower().Contains("music"))
            {
                _musicAS = t.GetComponent<AudioSource>();
            }
            else if(t.name.ToLower().Contains("sound"))
            {
                _soundAS = t.GetComponent<AudioSource>();
            }
        }
    }

    void Start()
    {
        // Initial volume values
        MasterVolume = .25f;
        MusicVolume = .5f;
        SoundVolume = .5f;
    }

    public void PlaySound(AudioClip clip)
    {
        _soundAS.pitch = 1f;
        _soundAS.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float pitch)
    {
        _soundAS.pitch = pitch;
        _soundAS.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip, bool looping = true)
    {
        _musicAS.loop = looping;
        _musicAS.clip = clip;
        _musicAS.Play();
    }

    public void StopMusic()
    {
        _musicAS.Stop();
    }

    public void SetMusicPaused(bool paused)
    {
        if(paused)
        {
            _musicAS.Pause();
        }else{
            _musicAS.UnPause();
        }
    }
}