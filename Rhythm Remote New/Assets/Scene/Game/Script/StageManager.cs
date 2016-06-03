using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public GameObject _blackMask;
    private AudioFunction _audio;
    private string _sceneName;

    void Awake()
    {
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
        _audio = GameObject.Find("AudioSource").GetComponent<AudioFunction>();
    }

    public void Retry()
    {
        _audio.Pause();
        GameObject.Find("Keys").SetActive(false);
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _sceneName = SceneManager.GetActiveScene().name;
        Invoke("LoadScene", 1.0f);
    }

    public void Exit()
    {
        _audio.Pause();
        GameObject.Find("Keys").SetActive(false);
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _sceneName = "Select";
        Invoke("LoadScene", 1.0f);
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
