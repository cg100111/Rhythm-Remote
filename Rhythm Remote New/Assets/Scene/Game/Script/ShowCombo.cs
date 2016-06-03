using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowCombo : MonoBehaviour {

    private int _showCombo = 0;
    private int _currentCombo = 0;
    private Text _text;
    private Text _shadow;
    private Animator _animator;
    private int _maxCombos = 0;

    void Start()
    {
        _text = this.GetComponent<Text>();
        _shadow = GameObject.Find("ComboShadow").GetComponent<Text>();
        _animator = this.GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        if (_showCombo != _currentCombo)
        {
            _showCombo = _currentCombo;
            string newText = _showCombo.ToString() + "x";
            _text.text = newText;
            _shadow.text = newText;
        }
	}

    public void AddCombo()
    {
        _currentCombo++;
        this.CheckMaxCombos();
        this.Play();
    }

    public void FailCombo()
    {
        _currentCombo = 0;
    }

    public void Play()
    {
        _animator.Play("ComboEffect", -1, 0f);
    }

    private void CheckMaxCombos()
    {
        if (_currentCombo > _maxCombos)
            _maxCombos = _currentCombo;
    }

    public int GetMaxCombos()
    {
        return _maxCombos;
    }
}
