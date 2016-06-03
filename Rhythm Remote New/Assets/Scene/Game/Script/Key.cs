using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    private float _approchRateTime = 1.0f;
    private int _targetTime;
    private bool _status = true;
    private ObjectPool _poolTarget;
    
    void FixedUpdate()
    {
        this.transform.Translate(0f, -250.4f * 0.008f * (1000.0f / _approchRateTime), 0f);
        if (this.transform.localPosition.y <= -138.5f)
            _status = false;
    }

    public void SetKeyInformation(float approchRateTime, int targetTime, ObjectPool poolObject)
    {
        _approchRateTime = approchRateTime;
        _targetTime = targetTime;
        _poolTarget = poolObject;
    }

    public bool GetKeyStatus()
    {
        return _status;
    }

    public int GetTargetTime()
    {
        return _targetTime;
    }

    public void Collection()
    {
        _poolTarget.CollectionObject(this.gameObject);
        _status = true;
    }
}