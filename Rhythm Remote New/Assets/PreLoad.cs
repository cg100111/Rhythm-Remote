using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreLoad : MonoBehaviour {

    void Awake()
    {
        PlayerPrefs.SetFloat("SongVolume", 0.5f);
        PlayerPrefs.SetFloat("SoundVolume", 1.0f);
        PlayerPrefs.SetInt("Offset", 0);
        GameObject sound = GameObject.Find("AudioCenter");
        HitSound hit = sound.GetComponent<HitSound>();
        hit.SetSound();
        DontDestroyOnLoad(sound);
        SceneManager.LoadSceneAsync("Index", LoadSceneMode.Single);
    }
}
