using UnityEngine;
using System.Collections;

public class AudioFunction : MonoBehaviour {

    private AudioSource _audio;
    private float _musicTime = 3.0f;
    private bool _start;

    void Awake()
    {
        _audio = this.GetComponent<AudioSource>();
        _start = false;
    }

    // Use this for initialization
    void Start () {
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
    }

    //音樂是否播放結束
    public bool IsFinish()
    {
        if (_start && !_audio.isPlaying)
            return true;
        return false;
    }

    //設定幾秒後開始撥放音樂
    public void SetPlayMusicAfterSeconds(float musicTime)
    {
        _musicTime = musicTime;
    }

    public float GetClipTime()
    {
        return _audio.clip.length;
    }

    public void Play()
    {
        _start = true;
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
            Debug.Log("set audio volume");
            _audio.volume = value;
        }
    }
}
