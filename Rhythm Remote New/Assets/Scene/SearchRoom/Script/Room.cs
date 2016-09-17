using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Room : MonoBehaviour {

    public SearchRoomManager _searchRoomManager;
    public Text _content;

    private string[] _ip;
    private string _roomName;
    private bool _havePassword;
    private int _port;

    public void Cennect()
    {
        if (!_havePassword)
        {
            Network.Connect(_ip, _port);
            _searchRoomManager.ChangeScene("Room");
        }
    }

    private void SetRoomName()
    {
        _content.text = _roomName;
    }

    public string RoomName
    {
        set
        {
            _roomName = value;
            SetRoomName();
        }
        get
        {
            return _roomName;
        }
    }

    public string[] IP
    {
        set
        {
            _ip = value;
        }
        get
        {
            return _ip;
        }
    }

    public int Port
    {
        set
        {
            _port = value;
        }
        get
        {
            return _port;
        }
    }

    public bool HavePassword
    {
        set
        {
            _havePassword = value;
        }
        get
        {
            return _havePassword;
        }
    }
}
