using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public KeyGeneratorFunction[] _keyGenerators;
    public Key[] _keyType;
    public GameObject _blackMask;
    public ShowCombo _combo;
    public ShowScore _score;
    public ShowAccEffect _effect;
    public float _startDelayTime = 1.5f;
    public float _endDelayTime = 1.5f;
    public float _approchRateTime = 1000.0f;

    private GameObject _keys;
    private List<List<int>> _keyQueue = new List<List<int>>();
    private AudioFunction _audio;
    private int _RhythmOffset = 0;

    void Awake()
    {
        Application.targetFrameRate = 60;
        _audio = GameObject.Find("AudioSource").GetComponent<AudioFunction>();
        _keys = GameObject.Find("Keys");
        _RhythmOffset = PlayerPrefs.GetInt("Offset");
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
            int time = _keyQueue[0][1];
            if (time <= _audio.GetAudioCurrentPlayTime() + _RhythmOffset)
            {
                AddKey(time);
            }
            if(_keyQueue.Count == 0)
                Invoke("EndGame", _endDelayTime);
        }
    }

    //產生跟time相同時間的所有節奏
    private void AddKey(int time)
    {
        int pos = 0;
        while (_keyQueue.Count != 0 && time == _keyQueue[0][1])
        {
            pos = _keyQueue[0][0];
            Key key = Instantiate(_keyType[pos], _keyGenerators[pos].transform.position, _keyGenerators[pos].transform.rotation) as Key;
            key.transform.SetParent(_keys.transform);
            key.SetKeyInformation(_approchRateTime, time + (int)_approchRateTime);
            _keyGenerators[pos].AddKeys(key);
            _keyQueue.RemoveAt(0);
        }
    }

    //載入譜面
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
            List<int> item = new List<int>();
            int rand = -1;
            while (true)
            {
                rand = this.RandomDigit(_keyGenerators.Length);
                if (offset == rand && offset != -1)
                    continue;
                else
                    break;
            }
            item.Add(rand);
            item.Add(time);
            _keyQueue.Add(item);
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
        SceneManager.LoadSceneAsync("Result", LoadSceneMode.Single);
    }
}
