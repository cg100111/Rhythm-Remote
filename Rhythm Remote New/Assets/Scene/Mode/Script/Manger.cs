using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Manger : MonoBehaviour {

    public GameObject _blackMask;
    public GameObject _multi;
    public GameObject _solo;
    public GameObject _createRoom;
    public GameObject _searchRoom;

    private int port = 8000;                    //隨意，範圍為1024~65536
    private string _sceneName;
    private bool _isDisplay = false;

    void Awake()
    {
        this.PCLoadHitSound();
    }

    void Start()
    {
        _blackMask.GetComponent<Image>().raycastTarget = false;
        SetButtonEnable();
    }

    private void PCLoadHitSound()
    {
        #if !UNITY_ANDROID || UNITY_EDITOR
            HitSound hit = GameObject.Find("AudioCenter").GetComponent<HitSound>();
            hit.SetSound();
        #else
        #endif
    }

    public void MultiPlayer()
    {
        _isDisplay = true;
        SetButtonEnable();
    }

    private void SetButtonEnable()
    {
        _createRoom.SetActive(_isDisplay);
        _searchRoom.SetActive(_isDisplay);
        _multi.SetActive(!_isDisplay);
        _solo.SetActive(!_isDisplay);
    }

    //進入房間頁面
    public void CreateRoom()
    {
        StartNetwork();
        _sceneName = "Room";
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    //進入單人模式
    public void Single()
    {
        _sceneName = "Select";
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    //進入搜尋房間畫面
    public void SearchRoom()
    {
        _sceneName = "SearchRoom";
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    //回起始頁
    public void BacktoIndex()
    {
        _sceneName = "Index";
        LeaveThisScreen();
        Invoke("LoadScene", 1.0f);
    }

    //建立server
    private void StartNetwork()
    {
        Debug.Log("initialize server");
        Network.InitializeServer(2, port, !Network.HavePublicAddress());
        //第一個參數:搜尋名稱 第二個參數: 房名
        MasterServer.RegisterHost("Rhythm Remote", "test", "CCC");
    }

    private void LeaveThisScreen()
    {
        _isDisplay = false;
        _createRoom.SetActive(_isDisplay);
        _searchRoom.SetActive(_isDisplay);
        _multi.SetActive(_isDisplay);
        _solo.SetActive(_isDisplay);
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _blackMask.GetComponent<Image>().raycastTarget = true;
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
