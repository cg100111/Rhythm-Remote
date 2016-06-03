using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public KeyGeneratorFunction[] _keyGenerators;
    public Key[] _keyType;
    private ObjectPool[] _objectPool;
    private AudioFunction _audio;
    public float _startDelayTime = 1.5f;
    public float _endDelayTime = 1.5f;
    public float _approchRateTime = 1000.0f;
    private GameObject _keys;
    private List<KeyTime> _keyQueue = new List<KeyTime>();
    private int _RhythmOffset = 0;

    public GameObject _blackMask;
    public ShowCombo _combo;
    public ShowScore _score;
    public ShowAccEffect _effect;

    void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;  //電腦用 Unity Editer 垂直同步 手機用 targetFrameRate
        #else
        #endif

        _audio = GameObject.Find("AudioSource").GetComponent<AudioFunction>();
        _keys = GameObject.Find("Keys");
        _RhythmOffset = PlayerPrefs.GetInt("Offset");

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

        this.Load();
    }

	// Use this for initialization
	void Start () {
        _audio.Volume = PlayerPrefs.GetFloat("SongVolume");
        Invoke("Play", _startDelayTime);
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
            if(_keyQueue.Count == 0)
                Invoke("EndGame", _endDelayTime);
        }
    }

    private void Load()
    {
        string songsText = PlayerPrefs.GetString("SongsTextData");
        TextAsset txt = Resources.Load<TextAsset>("SongsRhythmData/" + songsText);
        string[] text = txt.text.Split('\n');
        int offset = -1;
        for (int i = 0; i < text.Length; i++)
        {
            int time = int.Parse(text[i]);
            time -= (int)_approchRateTime;
            int rand = -1;
            while (true)
            {
                rand = this.RandomDigit(_keyGenerators.Length);
                if (offset == rand && offset != -1)
                    continue;
                else
                    break;
            }
            KeyTime key = new KeyTime(rand, time);
            _keyQueue.Add(key);
            offset = rand;
        }
    }

    private int RandomDigit(int num)
    {
        return Random.Range(0, num);
    }

    private void Play()
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
