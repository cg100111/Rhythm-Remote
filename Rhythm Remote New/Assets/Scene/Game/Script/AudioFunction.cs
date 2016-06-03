using UnityEngine;
using System.Collections;

public class AudioFunction : MonoBehaviour {

    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        _audio = this.GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Songs/" + PlayerPrefs.GetString("SongsTextData"));
        _audio.clip = clip;
	}

    public int GetAudioCurrentPlayTime()
    {
        return ((int)(_audio.time * 1000.0f));
    }

    public float GetClipTime()
    {
        return _audio.clip.length;
    }

    public void Play()
    {
        _audio.Play();
    }

    public void Pause()
    {
        _audio.Pause();
    }

    public float Volume
    {
        get
        {
            return _audio.volume;
        }

        set
        {
            _audio.volume = value;
        }
    }
}
