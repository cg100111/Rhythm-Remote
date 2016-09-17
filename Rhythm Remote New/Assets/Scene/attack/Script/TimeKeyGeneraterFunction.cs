using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeKeyGeneraterFunction : MonoBehaviour {

    public TimeKey _timeKey;
    public AudioFunction _audio;
    public AttackAPI _api;

    private ObjectPool _timeKeyPool;
    private List<TimeKey> _timeKeys;
    private int _RhythmOffset = 0;

    // Use this for initialization
    void Start () {
        _timeKeyPool = new ObjectPool(_timeKey.gameObject, 10);
        _timeKeys = new List<TimeKey>();
        _RhythmOffset = PlayerPrefs.GetInt("Offset");
    }
	
	// Update is called once per frame
	void Update () {
        if (_timeKeys.Count > 0)
            if(_timeKeys[0].transform.localPosition.x <= -220.0f)
                DeleteFirstTimeKey();
    }

    //新增timeKey
    public void AddKeys(float approchRateTime, int time)
    {
        TimeKey timeKey = _timeKeyPool.AccessObject(this.transform.position, this.transform.rotation).GetComponent<TimeKey>();
        timeKey.SetKeyInformation(approchRateTime, time + (int)approchRateTime);
        timeKey.transform.SetParent(GameObject.Find("TimeKeys").transform);
        _timeKeys.Add(timeKey);
    }

    public bool TouchKeys(string touchedObjectName)
    {
        if (_timeKeys.Count != 0 && _timeKeys[0].transform.localPosition.x <= -120.0f)
        {
            int offset = Mathf.Abs(_timeKeys[0].GetTargetTime() - _audio.GetAudioCurrentPlayTime() - _RhythmOffset);
            Debug.Log("offset : " + offset);
            if (offset <= 85)
            {
                SendTimeKeyToClient(touchedObjectName);
                return true;
            }
        }
        return false;
    }

    //把玩家按的keyBar送給守方
    private void SendTimeKeyToClient(string objectName)
    {
        _api.SendTimeKeyToClient(RPCMode.Others, objectName, _timeKeys[0].GetTargetTime());
    }

    //刪除第一個timeKey
    private void DeleteFirstTimeKey()
    {
        if (_timeKeys.Count > 0)
        {
            _timeKeyPool.CollectionObject(_timeKeys[0].gameObject);
            _timeKeys.RemoveAt(0);
        }
    }

    //刪除在時間點的timeKey
    //public void DeleteTimeKeyAtTime(int time)
    //{
    //    if(_timeKeys.Count > 0)
    //    {
    //        if (_timeKeys[0].GetTargetTime() == time)
    //        {
    //            //Debug.Log("點擊消除");
    //            _timeKeyPool.CollectionObject(_timeKeys[0].gameObject);
    //            _timeKeys.RemoveAt(0);
    //        }
    //    }
    //}
}
