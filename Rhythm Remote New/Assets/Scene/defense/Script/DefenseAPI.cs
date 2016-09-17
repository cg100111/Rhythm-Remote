using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAPI : MonoBehaviour {

    public NetworkView _netView;
    public Character _player;

    private string _pressedKeyBar;
    private int _time;

    public string GetPressedKeyBar()
    {
        return _pressedKeyBar;
    }

    public int GetPressedKeyTime()
    {
        return _time;
    }

    [RPC]
    private void SendTimeKey(string position, int time)
    {
        _pressedKeyBar = position;
        _time = time;
        _player.GetPressedKeyBar();
    }
}
