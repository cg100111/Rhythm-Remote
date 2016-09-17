using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Client : User {

    public RoomManager _manager;
    public Button _ready;
    public Button _quit;
    public Text _creator;
    public Text _participant;
    public Text _musicName;
    public API _api;

    private bool _isReady;
    private string _sceneName;

    private const  string IS_READY = "取消準備";
    private const string IS_NOT_READY = "準備";
    private const string PLAYER_NAME = "Client";

    // Use this for initialization
    void Start ()
    {
        Initialize();
        SendDataToServer();
    }

    private void Initialize()
    {
        _isReady = false;
        _ready = GameObject.Find("Ready").GetComponent<Button>();
        _quit = GameObject.Find("Quit").GetComponent<Button>();
        _manager = GameObject.Find("Manager").GetComponent<RoomManager>();
        _creator = GameObject.Find("Creator").GetComponent<Text>();
        _participant = GameObject.Find("Participant").GetComponent<Text>();
        _api = GameObject.Find("Manager").GetComponent<API>();
        _musicName = GameObject.Find("MusicName").GetComponent<Text>();
        _ready.onClick.AddListener(Ready);
        _quit.onClick.AddListener(Quit);
        _participant.text = PLAYER_NAME;
        _api.SetCanReceiveMessage(RPCMode.Server, true);
    }

    void OnConnectedToServer()
    {
        Debug.Log(" client connect");
        SendDataToServer();
    }

    //送資料給server
    private void SendDataToServer()
    {
        _api.SendPlayerName(RPCMode.Server, PLAYER_NAME);
        _api.SendIsReady(RPCMode.Server, _isReady);
    }

    //準備
    public void Ready()
    {
        _isReady = !_isReady;
        if (_isReady)
        {
            _ready.GetComponentInChildren<Text>().text = IS_READY;
        }
        else
        {
            _ready.GetComponentInChildren<Text>().text = IS_NOT_READY;
        }
        _api.SendIsReady(RPCMode.Server, _isReady);
    }

    //離開
    public void Quit()
    {
        Network.Disconnect();
        _manager.ChangeScrene("SearchRoom");
    }

    //當應用程式結束時
    private void OnApplicationQuit()
    {
        Network.Disconnect();//中斷伺服器連線，一旦伺服器中斷則客戶端也會自動中斷
    }

    public override void GetPlayerName()
    {
        _creator.text = _api.GetPlayerName();
    }

    public override void GetIsReady()
    {
    }

    public override void GetMusicName()
    {
        string name = _api.GetMusicName();
        _musicName.text = name;
        PlayerPrefs.SetString("SongsTextData", name);
    }

    public override void GetClientIsReady()
    {
    }

    public override void GetNextSceneName()
    {
        _manager.ChangeScrene(_api.GetNextSceneName());
    }
}
