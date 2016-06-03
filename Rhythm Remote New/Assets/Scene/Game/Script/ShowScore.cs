using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowScore : MonoBehaviour {

    private int _showScore = 0;
    private int _currentScore = 0;
    private Text _text;
    private Text _shadow;

    void Start()
    {
        _text = this.GetComponent<Text>();
        _shadow = GameObject.Find("ScoreShadow").GetComponent<Text>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (_showScore != _currentScore)
        {
            _showScore += 25;
            string newScore = this.RefreshScoreArray();
            _text.text = newScore;
            _shadow.text = newScore;
        }
	}

    public void SetScore(int score)
    {
        _currentScore += score;
    }

    public int GetScore()
    {
        return _currentScore;
    }

    private string RefreshScoreArray()
    {
        int digits = _showScore;

        char[] charArray = { '0', '0', '0', '0', '0', '0' };

        for (int i = 0; i < 6; i++)
        {
            if (digits > 0)
            {
                charArray[5 - i] = (digits % 10).ToString()[0];
                digits /= 10;
            }
        }
        return (new string(charArray));
    }
}
