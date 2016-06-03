using UnityEngine;
using System.Collections;

public class ShowAccEffect : MonoBehaviour {

    private Animator _animator;

    private int _threeHundredCount = 0;
    private int _oneHundredCount = 0;
    private int _fiftyCount = 0;
    private int _missCount = 0;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
	}

    public void ShowThreeHundred()
    {
        _animator.SetTrigger("acc300");
        _threeHundredCount++;
    }

    public void ShowOneHundred()
    {
        _animator.SetTrigger("acc100");
        _oneHundredCount++;
    }

    public void ShowFifty()
    {
        _animator.SetTrigger("acc50");
        _fiftyCount++;
    }

    public void ShowMiss()
    {
        _animator.SetTrigger("accMiss");
        _missCount++;
    }

    public int GetThreeHundredCount()
    {
        return _threeHundredCount;
    }

    public int GetOneHundredCount()
    {
        return _oneHundredCount;
    }

    public int GetFiftyCount()
    {
        return _fiftyCount;
    }

    public int GetMissCount()
    {
        return _missCount;
    }
}
