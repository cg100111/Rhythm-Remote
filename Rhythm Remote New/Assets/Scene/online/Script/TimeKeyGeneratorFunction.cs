using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeKeyGeneratorFunction : MonoBehaviour {


    private List<TimeKey> _keys = new List<TimeKey>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_keys.Count > 0)
        {
            if (!_keys[0].GetKeyStatus())
                KeyFail();
            //else if (_auto)
            //{
            //    if (_keys[0].transform.localPosition.y <= 2.9f)
            //    {
            //        if (_keys.Count == 1)
            //        {
            //            this.AutoPlay(0.1f);
            //        }
            //        else
            //        {
            //            float distance = _keys[1].transform.localPosition.y - _keys[0].transform.localPosition.y;
            //            float time = Mathf.Clamp((distance * 0.01f) / 2.0f, 0f, 0.1f);
            //            this.AutoPlay(time);
            //        }
            //    }
            //}
        }
    }

    public void AddKeys(TimeKey key)
    {
        _keys.Add(key);
    }

    //點擊失敗
    private void KeyFail()
    {
        Destroy(_keys[0].gameObject);
        _keys.RemoveAt(0);
        //_comboText.FailCombo();
        //_accEffect.ShowMiss();
    }
}
