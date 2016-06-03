using UnityEngine;
using System.Collections;

public class HitSound : MonoBehaviour {

    private int _hitSound;
    private float _soundVolume;

	// Use this for initialization
	void Start () {
        _hitSound = AudioCenter.loadSound("hitSound");
        _soundVolume = PlayerPrefs.GetFloat("SoundVolume");
	}

    public float Volume
    {
        get
        {
            return _soundVolume;
        }

        set
        {
            _soundVolume = value;
        }
    }

    public void PlaySound()
    {
        AudioCenter.playSound(_hitSound, _soundVolume);
    }
}
