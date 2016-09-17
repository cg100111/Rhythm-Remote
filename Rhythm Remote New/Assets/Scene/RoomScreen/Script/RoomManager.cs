using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {

    public GameObject _serverScreen;
    public GameObject _clientScreen;
    public GameObject _creator;
    public GameObject _participant;
    public GameObject _vs;
    public GameObject _blackMask;

    private string _sceneName;

    void Awake()
    {
        this.PCLoadHitSound();
        SetClientOrServer();
    }

    private void PCLoadHitSound()
    {
        #if !UNITY_ANDROID || UNITY_EDITOR
            HitSound hit = GameObject.Find("AudioCenter").GetComponent<HitSound>();
            hit.SetSound();
        #else
        #endif
    }

    private void SetClientOrServer()
    {
        Debug.Log(Network.peerType);
        if (Network.isServer)
        {
            _serverScreen.SetActive(true);
            _clientScreen.SetActive(false);
            this.gameObject.AddComponent<Server>();
        }
        if (Network.isClient)
        {
            _serverScreen.SetActive(false);
            _clientScreen.SetActive(true);
            this.gameObject.AddComponent<Client>();
        }
    }
    
    public void TestAttack()
    {
        ChangeScrene("Attack");
    }

    //切換畫面
    public void ChangeScrene(string sceneName)
    {
        _sceneName = sceneName;
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    private void LeaveThisScreen()
    {
        _participant.SetActive(false);
        _creator.SetActive(false);
        _serverScreen.SetActive(false);
        _clientScreen.SetActive(false);
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _blackMask.GetComponent<Image>().raycastTarget = true;
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
