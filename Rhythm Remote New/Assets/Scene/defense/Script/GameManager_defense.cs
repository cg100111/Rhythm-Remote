using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_defense : Character {

    public KeyGeneratorFunction[] _keyGenerators;
    public Key[] _keyType;
    public DefenseAPI _api;
    public GameObject _blackMask;
    public ShowCombo _combo;
    public ShowScore _score;
    public ShowAccEffect _effect;
    public float _startDelayTime = 1.5f;
    public float _endDelayTime = 1.5f;
    public float _approchRateTime = 1000.0f;

    private ObjectPool[] _objectPool;
    private AudioFunction _audio;
    private GameObject _keys;
    private List<KeyTime> _keyQueue = new List<KeyTime>();
    private int _RhythmOffset = 0;

    void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;  //電腦用 Unity Editer 垂直同步 手機用 targetFrameRate
        #else
        #endif

        _audio = GameObject.Find("AudioSource").GetComponent<AudioFunction>();
        _keys = GameObject.Find("Keys");
        _RhythmOffset = PlayerPrefs.GetInt("Offset");
        _audio.SetPlayMusicAfterSeconds(_startDelayTime);

        _objectPool = new ObjectPool[7]
        {
           new ObjectPool(_keyType[0].gameObject, 10),
           new ObjectPool(_keyType[1].gameObject, 10),
           new ObjectPool(_keyType[2].gameObject, 10),
           new ObjectPool(_keyType[3].gameObject, 10),
           new ObjectPool(_keyType[4].gameObject, 10),
           new ObjectPool(_keyType[5].gameObject, 10),
           new ObjectPool(_keyType[6].gameObject, 10),
        };
    }

    // Use this for initialization
    void Start () {
        _audio.Volume = PlayerPrefs.GetFloat("SongVolume");
        Invoke("PlayMusic", _startDelayTime);
    }

    void FixedUpdate()
    {
        if (_keyQueue.Count != 0)
        {
            while (true)
            {
                int pos = _keyQueue[0].GetPos();
                int time = _keyQueue[0].GetTime();
                if (time <= _audio.GetAudioCurrentPlayTime() + _RhythmOffset)
                {
                    Key key = _objectPool[pos].AccessObject(_keyGenerators[pos].transform.position, _keyGenerators[pos].transform.rotation).GetComponent<Key>();
                    key.transform.SetParent(_keys.transform);
                    key.SetKeyInformation(_approchRateTime, time + (int)_approchRateTime, _objectPool[pos]);
                    _keyGenerators[pos].AddKeys(key);
                    _keyQueue.RemoveAt(0);
                    if (_keyQueue.Count != 0 && time == _keyQueue[0].GetTime())
                        continue;
                }
                break;
            }
        }
    }

    //去取得攻方送來的keyBar和點擊時間
    public override void GetPressedKeyBar()
    {
        AnalyzePressedKeys(_api.GetPressedKeyBar(), _api.GetPressedKeyTime());
    }

    //分析攻方送來的keybar有哪些並轉成守方的keybar
    private void AnalyzePressedKeys(string pressedKey, int time)
    {
        if (pressedKey.Contains("keyBarPurpleLeft"))
            AddKeyToKeyQueue(0, time);
        else if (pressedKey.Contains("keyBarGreenLeft"))
            AddKeyToKeyQueue(1, time);
        else if (pressedKey.Contains("keyBarBlueLeft"))
            AddKeyToKeyQueue(2, time);
        else if (pressedKey.Contains("keyBarWhiteMiddle"))
            AddKeyToKeyQueue(3, time);
        else if (pressedKey.Contains("keyBarBlueRight"))
            AddKeyToKeyQueue(4, time);
        else if (pressedKey.Contains("keyBarGreenRight"))
            AddKeyToKeyQueue(5, time);
        else if (pressedKey.Contains("keyBarPurpleRight"))
            AddKeyToKeyQueue(6, time);
    }

    //產生相對應位置的key並丟到keyQueue裡
    private void AddKeyToKeyQueue(int position,int time)
    {
        KeyTime key = new KeyTime(position, time);
        _keyQueue.Add(key);
    }

    private void PlayMusic()
    {
        _audio.Play();
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("Score", _score.GetScore());
        PlayerPrefs.SetInt("MaxCombos", _combo.GetMaxCombos());
        PlayerPrefs.SetInt("ThreeHundredCount", _effect.GetThreeHundredCount());
        PlayerPrefs.SetInt("OneHundredCount", _effect.GetOneHundredCount());
        PlayerPrefs.SetInt("FiftyCount", _effect.GetFiftyCount());
        PlayerPrefs.SetInt("MissCount", _effect.GetMissCount());
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        Invoke("LoadResult", 1.0f);
    }

    private void LoadResult()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync("Result", LoadSceneMode.Single);
    }
}
