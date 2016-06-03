using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeyGeneratorFunction : MonoBehaviour {

    private List<Key> _keys = new List<Key>();
    private ShowScore _scoreText;
    private ShowCombo _comboText;
    private AudioFunction _audio;
    private ShowAccEffect _accEffect;
    public GameObject _hitEffect;
    private ObjectPool _effectPool;
    public keyBarFunction _keyBar;

    private int _RhythmOffset = 0;
    private bool _auto = false;

    void Awake()
    {
        _effectPool = new ObjectPool(_hitEffect, 20);
    }

    void Start()
    {
        _RhythmOffset = PlayerPrefs.GetInt("Offset");
        _scoreText = GameObject.Find("ScoreDisplay").GetComponent<ShowScore>();
        _comboText = GameObject.Find("ComboDisplay").GetComponent<ShowCombo>();
        _audio = GameObject.Find("AudioSource").GetComponent<AudioFunction>();
        _accEffect = GameObject.Find("AccEffect").GetComponent<ShowAccEffect>();
        _keyBar = this.transform.GetChild(0).GetComponent<keyBarFunction>();
        if (PlayerPrefs.GetInt("AutoPlay") == 1)
            _auto = true;
    }

    void FixedUpdate()
    {
        
        if (_keys.Count > 0)
        {
            if (!_keys[0].GetKeyStatus())
                KeyFail();
            else if (_auto)
            {
                if (_keys[0].transform.localPosition.y <= -100.2f)
                {
                    if (_keys.Count == 1)
                    {
                        this.AutoPlay(0.1f);
                    }
                    else
                    {
                        float distance = _keys[1].transform.localPosition.y - _keys[0].transform.localPosition.y;
                        float time = Mathf.Clamp((distance * 0.01f) / 2.0f, 0f, 0.1f);
                        this.AutoPlay(time);
                    }
                }
            }
        }
    }

    public void AddKeys(Key key)
    {
        _keys.Add(key);
    }

    public void TouchKeys()
    {
        if (_keys.Count > 0 && _keys[0].transform.localPosition.y <= -10.0f)
        {
            int offset = Mathf.Abs(_keys[0].GetTargetTime() - _audio.GetAudioCurrentPlayTime() - _RhythmOffset);
            if (offset <= 60 || _auto)
            {
                _scoreText.SetScore(300);
                _accEffect.ShowThreeHundred();
            }
                
            else if (offset <= 85)
            {
                _scoreText.SetScore(100);
                _accEffect.ShowOneHundred();
            }
               
            else if (offset <= 110)
            {
                _scoreText.SetScore(50);
                _accEffect.ShowFifty();
            }
                
            else
            {
                this.SetEffect();
                this.KeyFail();
                return;
            }

            this.SetEffect();
            _keys[0].Collection();
            _keys.RemoveAt(0);
            _comboText.AddCombo();
        }
    }

    private void KeyFail()
    {
        _keys[0].Collection();
        _keys.RemoveAt(0);
        _comboText.FailCombo();
        _accEffect.ShowMiss();
    }

    private void SetEffect()
    {
        HitEffect effect = _effectPool.AccessObject(_keys[0].transform.position, _keys[0].transform.rotation).GetComponent<HitEffect>();
        effect.SetPoolTarget(_effectPool);
        effect.transform.SetParent(this.transform.parent.transform);
        effect.Play();
    }

    //Auto
    private void AutoPlay(float time)
    {
        _keyBar.AutoDown(time);
    }
}
