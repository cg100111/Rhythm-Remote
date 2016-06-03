using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeyGeneratorFunction : MonoBehaviour {

    public GameObject _hitEffect;
    public keyBarFunction _keyBar;

    private List<Key> _keys = new List<Key>();
    private ShowScore _scoreText;
    private ShowCombo _comboText;
    private AudioFunction _audio;
    private ShowAccEffect _accEffect;
    private int _RhythmOffset = 0;
    private bool _auto = false;

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
                if (_keys[0].transform.localPosition.y <= 2.9f)
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

    //點擊判定
    public void TouchKeys()
    {
        if (_keys.Count > 0 && _keys[0].transform.position.y < 110.0f)
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
            Destroy(_keys[0].gameObject);
            _keys.RemoveAt(0);
            _comboText.AddCombo();
        }
    }

    //點擊失敗
    private void KeyFail()
    {
        Destroy(_keys[0].gameObject);
        _keys.RemoveAt(0);
        _comboText.FailCombo();
        _accEffect.ShowMiss();
    }

    private void SetEffect()
    {
        GameObject effect = Instantiate(_hitEffect, _keys[0].transform.position, _keys[0].transform.rotation) as GameObject;
        effect.transform.SetParent(this.transform.parent.transform);
    }

    //Auto
    private void AutoPlay(float time)
    {
        _keyBar.AutoDown(time);
    }
}
