using UnityEngine;
using System.Collections;

public class TimeKey : MonoBehaviour {

    private float _approchRateTime = 1.0f;
    private int _targetTime;

    void FixedUpdate()
    {
        move();
    }

    //移動
    private void move()
    {
        this.transform.Translate(-470.4f * 0.008f * (1000.0f / _approchRateTime), 0f, 0f);
    }

    //設定資訊
    public void SetKeyInformation(float approchRateTime, int targetTime)
    {
        _approchRateTime = approchRateTime;
        _targetTime = targetTime;
    }

    public int GetTargetTime()
    {
        return _targetTime;
    }
}
