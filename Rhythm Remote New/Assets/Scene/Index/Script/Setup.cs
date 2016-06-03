using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Setup : MonoBehaviour {

    private Animator _animator;
    public AudioSource _song;

    private bool _isOpen = false;

    private HitSound _sound;
    public Slider _sliderSong;
    public Slider _sliderSound;
    public Offset _offsetMenu;

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _animator.Play("SetupAnimationReturn", -1, 1);
    }

	// Use this for initialization
	void Start () {
        _sound = GameObject.Find("AudioCenter").GetComponent<HitSound>();
	}

    public void StopAll()
    {
        _offsetMenu.OnEndClick();
        _song.Stop();
        if (_isOpen)
            this.CallSetupMenu();
    }

    public void CallSetupMenu()
    {
        _animator.SetBool("Launch", true);
        _isOpen = !_isOpen;
    }

    public void TestSongVolume()
    {
        _song.volume = _sliderSong.value;
        _song.Play();
        CancelInvoke("StopSongPlay");
        Invoke("StopSongPlay", 3.8f);
    }

    private void StopSongPlay()
    {
        _song.Stop();
    }

    public void TestSoundVolume()
    {
        _sound.Volume = _sliderSound.value;
        _sound.PlaySound();
    }
}
