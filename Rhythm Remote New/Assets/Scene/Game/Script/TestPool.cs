using UnityEngine;
using System.Collections;

public class TestPool : MonoBehaviour {

    public GameObject _testObj;
    private ObjectPool _pool;

    void Awake()
    {
        _pool = new ObjectPool(_testObj, 30);
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
