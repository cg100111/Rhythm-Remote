using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Index : MonoBehaviour {

    private string _sceneName;
    public GameObject _blackMask;
    public Setup _setupMenu;

    void Awake()
    {
        _setupMenu._sliderSong.value = PlayerPrefs.GetFloat("SongVolume");
        _setupMenu._sliderSound.value = PlayerPrefs.GetFloat("SoundVolume");
        this.PCLoadHitSound();
    }

    private void PCLoadHitSound()
    {
        #if !UNITY_ANDROID || UNITY_EDITOR
            HitSound hit = GameObject.Find("AudioCenter").GetComponent<HitSound>();
            hit.SetSound();
        #else
        #endif
    } 

    void Start()
    {
        _blackMask.GetComponent<Image>().raycastTarget = false;
    }

    public void StartGame()
    {
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _sceneName = "Select";
        PlayerPrefs.SetFloat("SongVolume", _setupMenu._sliderSong.value);
        PlayerPrefs.SetFloat("SoundVolume", _setupMenu._sliderSound.value);
        PlayerPrefs.SetInt("Offset", _setupMenu._offsetMenu.GetOffset());
        _setupMenu.StopAll();
        Invoke("LoadScene", 1.0f);
    }

    public void ExitGame()
    {
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _setupMenu.StopAll();
        Invoke("Quit", 1.0f);
    }

    public void OnSetupClick()
    {
        _setupMenu.CallSetupMenu();
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
