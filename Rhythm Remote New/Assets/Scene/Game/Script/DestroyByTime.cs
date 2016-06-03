using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float _time = 1.5f;

	// Use this for initialization
	void Start () {
        Invoke("RemoveThisObject", _time);
	}
	
    void RemoveThisObject()
    {
        Destroy(this.gameObject);
    }
}
