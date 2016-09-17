using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAPI : MonoBehaviour {

    public NetworkView _netView;

    public void SendTimeKeyToClient(RPCMode target, string position, int time)
    {
        _netView.RPC("SendTimeKey", target, position, time);
    }

    [RPC]
    private void SendTimeKey(string position, int time)
    {

    }
}
