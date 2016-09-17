using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display : MonoBehaviour {

    public GameObject[] _displayRoom;
    public GameObject _nextPageButton;
    public GameObject _lastPageButton;
    public SearchRoomManager _searchRoomManager;

    private int _nowPage;
    private int _lastPage;
    private int _pageLimit;

	// Use this for initialization
	void Start () {
        _nowPage = 0;
        _pageLimit = 6;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddRoom(HostData[] roomData)
    {
        if ((roomData.Length % _pageLimit) != 0)
            _lastPage = (roomData.Length / _pageLimit) + 1;
        else
            _lastPage = roomData.Length / _pageLimit;
        for (int i = _nowPage*_pageLimit; i< (_nowPage + 1) * _pageLimit; i++)
        {
            //取資料
            if (i < roomData.Length)
            {
                _displayRoom[i].GetComponent<Room>().IP = roomData[i].ip;
                _displayRoom[i].GetComponent<Room>().RoomName = roomData[i].gameName;
                _displayRoom[i].GetComponent<Room>().Port = roomData[i].port;
                _displayRoom[i].GetComponent<Room>().HavePassword = roomData[i].passwordProtected;
                SetRoomEnable(i, true);
            }
            else
            {
                SetRoomEnable(i, false);
            }
        }
    }

    private void SetRoomEnable(int roomNumber, bool enable)
    {
        _displayRoom[(roomNumber % _pageLimit)].SetActive(enable);
    }

    public void GotoNextPage()
    {
        if(_nowPage < _lastPage)
            _nowPage++;
        _searchRoomManager.UpdateRoom();
    }

    public void GotoLastPage()
    {
        if (_nowPage > 0)
            _nowPage--;
        _searchRoomManager.UpdateRoom();
    }
}
