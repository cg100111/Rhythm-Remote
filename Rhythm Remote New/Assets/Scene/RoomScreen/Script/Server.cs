using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Server : User {

    public RoomManager _manager;
    public Dropdown _songMenu;
    public Button _start;
    public Button _disband;
    public API _api;
    public Text _creator;
    public Text _participant;

    private List<string> _songNames = new List<string>();
    private string _sceneName;
    private bool _clientIsReady;
    private bool _clientCanReceiveMessage;
    private bool _isFirstSend;

    private const string PLAYER_NAME = "Server";

    // Use this for initialization
    void Awake ()
    {
        Initialize();
        LoadSongName();
        PlayerPrefs.SetString("SongsTextData", _songNames[_songMenu.value]);
    }

    // Update is called once per frame
    void Update () {
        if (Network.connections.Length > 0 && _clientCanReceiveMessage)
            SendDataToClient();
    }

    private void Initialize()
    {
        _creator = GameObject.Find("Creator").GetComponent<Text>();
        _participant = GameObject.Find("Participant").GetComponent<Text>();
        _songMenu = GameObject.Find("SongMenu").GetComponent<Dropdown>();
        _api = GameObject.Find("Manager").GetComponent<API>();
        _start = GameObject.Find("Start").GetComponent<Button>();
        _disband = GameObject.Find("Disband").GetComponent<Button>();
        _manager = GameObject.Find("Manager").GetComponent<RoomManager>();
        _start.onClick.AddListener(StartGame);
        _disband.onClick.AddListener(Disband);
        _songMenu.onValueChanged.AddListener(delegate { ChangeSong(0); });
        _creator.text = PLAYER_NAME;
        _clientIsReady = false;
        _clientCanReceiveMessage = false;
        _isFirstSend = true;
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        ClearClientData();
    }

    //送資料給client
    private void SendDataToClient()
    {
        if (_isFirstSend)
        {
            _api.SendPlayerName(RPCMode.Others, PLAYER_NAME);
            _api.SendMusicName(RPCMode.Others, _songNames[_songMenu.value]);
            _isFirstSend = false;
        }
    }

    //清除client端資料
    private void ClearClientData()
    {
        _participant.text = "";
        _clientIsReady = false;
        _clientCanReceiveMessage = false;
        _isFirstSend = true;
    }

    //載入歌曲
    private void LoadSongName()
    {
        string[] splitFile = new string[] { "\r\n", "\r", "\n" };
        TextAsset txt = Resources.Load<TextAsset>("SongsTextData");
        string[] text = txt.text.Split(splitFile, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string sonName in text)
            _songNames.Add(sonName);
        _songMenu.ClearOptions();
        _songMenu.AddOptions(_songNames);
    }

    //換歌
    public void ChangeSong(int key)
    {
        PlayerPrefs.SetString("SongsTextData", _songNames[_songMenu.value]);
        _api.SendMusicName(RPCMode.Others, _songNames[_songMenu.value]);
    }

    //開始遊戲
    public void StartGame()
    {
        if (_clientIsReady)
        {
            _api.SetNextSceneName(RPCMode.Others, "Defensive");
            _manager.ChangeScrene("Attack");
        }
    }

    //解散
    public void Disband()
    {
        MasterServer.UnregisterHost();
        if (Network.connections.Length > 0)
            _api.SetNextSceneName(RPCMode.Others, "SearchRoom");
        Network.Disconnect();
        _manager.ChangeScrene("Mode");
    }

    //當應用程式結束時
    private void OnApplicationQuit()
    {
        Network.Disconnect();//中斷伺服器連線，一旦伺服器中斷則客戶端也會自動中斷
    }

    public override void GetIsReady()
    {
        _clientIsReady = _api.GetIsReady();
    }

    public override void GetPlayerName()
    {
        _participant.text = _api.GetPlayerName();
    }

    public override void GetMusicName()
    {
    }

    public override void GetClientIsReady()
    {
        _clientCanReceiveMessage = _api.IsClientCanReceiveMessage();
    }

    public override void GetNextSceneName()
    {
    }
}
