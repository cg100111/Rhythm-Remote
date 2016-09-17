using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchRoomManager : MonoBehaviour {

    public GameObject _blackMask;
    public Display _roomDisplay;
    public GameObject _nextPageButton;
    public GameObject _lastPageButton;

    private string _sceneName;

    // Use this for initialization
    void Start () {
        MasterServer.ClearHostList();
        InvokeRepeating("UpdateRoom", 0.0f, 5.0f);
        _sceneName = "";
    }
	
	// Update is called once per frame
	void Update () {

    }

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

    public void UpdateRoom()
    {
        MasterServer.RequestHostList("Rhythm Remote");//搜尋名為Rhythm Remote的Server
        HostData[] data = MasterServer.PollHostList();
        Debug.Log("data length : " + data.Length);
        _roomDisplay.AddRoom(data);
    }

    //當應用程式結束時
    void OnApplicationQuit()
    {
        Network.Disconnect();
    }

    //回起始頁
    public void BacktoIndex()
    {
        ChangeScene("Index");
    }

    //回上一頁
    public void Back()
    {
        ChangeScene("Mode");
    }

    public void ChangeScene(string sceneName)
    {
        _sceneName = sceneName;
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    private void LeaveThisScreen()
    {
        _nextPageButton.SetActive(false);
        _lastPageButton.SetActive(false);
        _roomDisplay.gameObject.SetActive(false);
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _blackMask.GetComponent<Image>().raycastTarget = true;
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
