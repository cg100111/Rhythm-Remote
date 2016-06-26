using UnityEngine;
using System.Collections;

public class AudioFunction : MonoBehaviour {

    private AudioSource _audio;
    private float _musicTime = 3;

    // Use this for initialization
    void Start () {
        _audio = this.GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Songs/" + PlayerPrefs.GetString("SongsTextData"));
        _audio.clip = clip;
	}

    void Update()
    {
        StartMusic();
    }

    //倒數到0時,在開始撥放音樂
    private void StartMusic()
    {
        if (!_audio.isPlaying)
        {
            if (_musicTime > 0)
                _musicTime -= Time.deltaTime;
            else
                _audio.Play();
        }
    }

    //取得現在音樂時間
    public int GetAudioCurrentPlayTime()
    {
        if (!_audio.isPlaying)
            return -(int)(_musicTime * 1000.0f);
        else
            return (int)(_audio.time * 1000.0f);
        //return ((int)(_audio.time * 1000.0f));
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
