using UnityEngine;
using System.Collections;

public class API : MonoBehaviour {

    public NetworkView _netView;         //必須先建立物件，才能使用RPC

    private User _user;
    private bool _clientCanReceiveMessage;
    private bool _isRaedy;
    private string _name;
    private string _musciName;
    private string _nextSceneName;

    void Start()
    {
        _clientCanReceiveMessage = false;
        _user = GameObject.Find("Manager").GetComponent<User>();
    }

    public void SendPlayerName(RPCMode target, string name)
    {
        _netView.RPC("Name", target, name);
    }

    public void SendMusicName(RPCMode target, string name)
    {
        _netView.RPC("Music", target, name);
    }

    public void SendIsReady(RPCMode target, bool isReady)
    {
        _netView.RPC("ClientIsReady", target, isReady);
    }

    public void SetCanReceiveMessage(RPCMode target, bool can)
    {
        _netView.RPC("ClientCanReceiveMessage", target, can);
    }

    public void SetNextSceneName(RPCMode target, string nextSceneName)
    {
        _netView.RPC("NextSceneName", target, nextSceneName);
    }

    public string GetPlayerName()
    {
        return _name;
    }

    public bool GetIsReady()
    {
        return _isRaedy;
    }

    public string GetMusicName()
    {
        return _musciName;
    }

    public string GetNextSceneName()
    {
        return _nextSceneName;
    }

    public bool IsClientCanReceiveMessage()
    {
        return _clientCanReceiveMessage;
    }

    //這裡負責接收並存下來通知對方來取資料
    [RPC]
    private void Name(string playerName)
    {
        _name = playerName;
        _user.GetPlayerName();
    }

    [RPC]
    private void Music(string musicName)
    {
        _musciName = musicName;
        _user.GetMusicName();
    }

    [RPC]
    private void ClientIsReady(bool isReady)
    {
        _isRaedy = isReady;
        _user.GetIsReady();
    }

    [RPC]
    private void ClientCanReceiveMessage(bool can)
    {
        _clientCanReceiveMessage = can;
        _user.GetClientIsReady();
    }

    [RPC]
    private void NextSceneName(string sceneName)
    {
        _nextSceneName = sceneName;
        _user.GetNextSceneName();
    }
}
