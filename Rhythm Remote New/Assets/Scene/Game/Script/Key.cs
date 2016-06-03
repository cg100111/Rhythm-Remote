using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    private float _approchRateTime;
    private int _targetTime;
    private bool _status = true;
    

    void FixedUpdate()
    {
        this.transform.Translate(0f, -250.1f * Time.fixedDeltaTime * (1000.0f / _approchRateTime), 0f);
        if (this.transform.position.y < -3.0f)
            _status = false;
    }

    public void SetKeyInformation(float approchRateTime, int targetTime)
    {
        _approchRateTime = approchRateTime;
        _targetTime = targetTime;
    }

    public bool GetKeyStatus()
    {
        return _status;
    }

    public int GetTargetTime()
    {
        return _targetTime;
    }
}
