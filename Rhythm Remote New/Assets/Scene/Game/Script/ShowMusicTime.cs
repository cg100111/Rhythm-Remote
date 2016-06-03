using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMusicTime : MonoBehaviour {

    private Text _text;
    private AudioSource _audio;

    void Start()
    {
        _text = this.GetComponent<Text>();
        _audio = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        _text.text = this.GetAudioCurrentTime().ToString();
    }

    public int GetAudioCurrentTime()
    {
        return (int)(_audio.time * 1000.0f);
    }
}
