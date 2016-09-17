using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Attack : MonoBehaviour {

    public TimeKeyGeneraterFunction _timeKeyGenerator;
    public AudioFunction _audio;
    public ShowAccEffect _effect;
    public GameObject _blackMask;
    public float _startDelayTime = 3.0f;
    public float _endDelayTime = 1.5f;
    public float _approchRateTime = 1000.0f;

    private List<int> _timeKeyTime;
    private int _RhythmOffset = 0;

    void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;  //電腦用 Unity Editer 垂直同步 手機用 targetFrameRate
        #else
        #endif

        _RhythmOffset = PlayerPrefs.GetInt("Offset");
        _timeKeyTime = new List<int>();
        
        this.Load();
    }

    // Use this for initialization
    void Start ()
    {
        _audio.Volume = PlayerPrefs.GetFloat("SongVolume");
        Invoke("Play", _startDelayTime);
    }

    // Update is called once per frame
    void Update () {
        if (_timeKeyTime.Count != 0)
        {
            int time = _timeKeyTime[0];
            if (time <= _audio.GetAudioCurrentPlayTime() + _RhythmOffset)
            {
                _timeKeyGenerator.AddKeys(_approchRateTime, time);
                _timeKeyTime.RemoveAt(0);
            }
        }
    }

    //載入
    private void Load()
    {
        string songsText = PlayerPrefs.GetString("SongsTextData");
        TextAsset txt = Resources.Load<TextAsset>("SongsRhythmData/" + songsText);
        string[] text = txt.text.Split('\n');
        int time = 0;
        for (int i = 0; i < text.Length; i++)
        {
            //同一時間點上只會有一個timeKey出現
            if (time != int.Parse(text[i]))
            {
                time = int.Parse(text[i]);
                _timeKeyTime.Add(time - (int)_approchRateTime);
            }
        }
    }

    //跑音樂
    private void Play()
    {
        _audio.Play();
    }

    //結束遊戲
    private void EndGame()
    {
        //PlayerPrefs.SetInt("Score", _score.GetScore());
        //PlayerPrefs.SetInt("MaxCombos", _combo.GetMaxCombos());
        PlayerPrefs.SetInt("ThreeHundredCount", _effect.GetThreeHundredCount());
        PlayerPrefs.SetInt("OneHundredCount", _effect.GetOneHundredCount());
        PlayerPrefs.SetInt("FiftyCount", _effect.GetFiftyCount());
        PlayerPrefs.SetInt("MissCount", _effect.GetMissCount());
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        //Invoke("LoadResult", 1.0f);
    }

    private void LoadResult()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync("Result", LoadSceneMode.Single);
    }
}
