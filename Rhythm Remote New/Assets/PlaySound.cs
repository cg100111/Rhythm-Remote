using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour,IPointerClickHandler {

    private int _soundId;

	// Use this for initialization
	void Start () {
        _soundId = AudioCenter.loadSound("hitSound");
	}

    public void OnPointerClick(PointerEventData data)
    {
        //AudioCenter.playSound(_soundId);
    }
}
