using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Offset : MonoBehaviour {

    public GameObject _key;
    public AudioSource _audio;
    public Text _textOffset;

    public float _reapeatTime = 1.0f;
    private bool _start = false;

	// Use this for initialization
	void Start () {
        _textOffset.text = PlayerPrefs.GetInt("Offset").ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(_start)
        {
            _key.transform.Translate(0f, -76.4f * Time.fixedDeltaTime * (1.0f / _reapeatTime - (float.Parse(_textOffset.text) / 1000.0f)), 0f);
        }
	}

    public int GetOffset()
    {
        return int.Parse(_textOffset.text);
    }

    public void OnStartClick()
    {
        _start = true;
        Invoke("PlaySound", _reapeatTime);
    }

    public void OnEndClick()
    {
        _start = false;
        CancelInvoke("PlaySound");
        CancelInvoke("Redo");
        _key.transform.localPosition = new Vector3(80.0f, 0f, 0f);
    }

    private void PlaySound()
    {
        _audio.Play();
        Invoke("Redo", _reapeatTime * 2.0f);
    }

    private void Redo()
    {
        _key.transform.localPosition = new Vector3(80.0f, 0f, 0f);
        Invoke("PlaySound", _reapeatTime);
    }

    public void OnPlusClick()
    {
        int num = int.Parse(_textOffset.text);
        num+=10;
        if (num > 1000)
            num = 1000;
        _textOffset.text = num.ToString();
    }

    public void OnMinusClick()
    {
        int num = int.Parse(_textOffset.text);
        num-=10;
        if (num < -1000)
            num = -1000;
        _textOffset.text = num.ToString();
    }
}
