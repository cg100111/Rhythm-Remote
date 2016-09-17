using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiGameManager : MonoBehaviour {

    public AudioFunction _audio;
    public GameObject _blackMask;
    public ShowCombo _combo;
    public ShowScore _score;
    public ShowAccEffect _effect;
    public float _startDelayTime = 1.5f;
    public float _endDelayTime = 1.5f;
    public float _approchRateTime = 1000.0f;

    private List<List<KeyTime>> _keys;
    private int _RhythmOffset = 0;
    private string _character;

    private const string ATTACKER = "Attacker";
    private const string DEFENDER = "Defender";

    void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;  //電腦用 Unity Editer 垂直同步 手機用 targetFrameRate
        #else
        #endif

        _RhythmOffset = PlayerPrefs.GetInt("Offset");
        _keys = new List<List<KeyTime>>();
        Load();
    }

    // Use this for initialization
    void Start () {
        _audio.Volume = PlayerPrefs.GetFloat("SongVolume");
        _character = "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //載入
    private void Load()
    {
        string songsText = PlayerPrefs.GetString("SongsTextData");
        TextAsset txt = Resources.Load<TextAsset>("SongsRhythmData/" + songsText);
        string[] text = txt.text.Split('\n');
        int time = -1;
        for (int i = 0; i < text.Length; i++)
        {
            string[] content = text[i].Split(' ');
            for(int item = 0; item < content.Length; item++)
            {
                if(time)
            }
            //同一時間點上只會有一個timeKey出現
            if (time != int.Parse(text[i]))
            {
                time = int.Parse(text[i]);
                _timeKeyTime.Add(time - (int)_approchRateTime);
            }
        }
    }
}
